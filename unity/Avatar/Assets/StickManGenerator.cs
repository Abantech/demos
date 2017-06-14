using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

public class StickManGenerator : AvatarGenerator
{
    //public BodyJointPositionMapping MoCapDataSource;
    public GameObject HeadAnchorObject;
    public GameObject RightHandAnchorObject;
    public float jointSize = .1f;
    public Material jointMaterial;
    public BodyJointPositionMapping BodyCaptureSource;

    private Dictionary<HumanJointType, GameObject> jointGameObjects;
    private List<Bone> bones;
    private bool figureGenerated = false;

    //Use this to find the stabilized position of joints
    private Dictionary<HumanJointType, List<Vector3>> JointPositionsLog;
    //Defines how many entries are stored for a given joint before it is removed
    public int JointPositionsMaxLogCount = 60;


    //Some Motion Captures do mirror image, adjust these to do the inverse to compensate as required
    public bool invertMocapX;
    public bool invertMocapY;
    public bool invertMocapZ;

    private bool searching = true;


    public void Start()
    {
        jointGameObjects = new Dictionary<HumanJointType, GameObject>();
    }

    private BodyJointPositionMapping MoCapDataSource
    {
        get
        {
            return BodyCaptureSource ?? KinectJointPositionMapper.Instance;
        }
    }

    public void LateUpdate()
    {
        if (figureGenerated)
        {
            CalculateOffset();

            bones.ForEach(bone => Bone.Update(bone));
        }
        else
        {
            if (MoCapDataSource != null && MoCapDataSource.IsInitialized)
            {
                CreateJoints();
                //Create joints and bones for the first time
                CreateBones();
                bones.ForEach(bone => Bone.Update(bone));
                figureGenerated = true;
            }
        }
    }

    private void CalculateOffset()
    {
        var headPosition = jointGameObjects[HumanJointType.Head].transform.position;

        var rotAroundy = -transform.eulerAngles.y;

        // Convert degrees to rads
        var rads = Math.PI * rotAroundy / 180.0;

        // Create x coordinate as a function of the rotation of the kinect
        var cameraForKinectx = Math.Cos(rads) * HeadAnchorObject.transform.position.x + Math.Sin(rads) * HeadAnchorObject.transform.position.z;

        var cameraForKinecty = HeadAnchorObject.transform.position.y;

        // Create z coordinate as a function of the rotation of the kinect
        var cameraForKinectz = Math.Sin(rads) * HeadAnchorObject.transform.position.x - Math.Cos(rads) * HeadAnchorObject.transform.position.z;

        // Create offset
        MoCapDataSource.Offset = MoCapDataSource.Offset + Vector3.Scale(new Vector3(-1, -1, -1), MoCapDataSource.HeadPosition) + new Vector3((float)cameraForKinectx, cameraForKinecty, (float)cameraForKinectz);

        //Update the joint positions
        foreach (var jointType in BodyJointPositionMapping.GetAllJointTypes())
        {
            Vector3 newJointPosition = Vector3.zero;
            //var jointType = HumanJointTypeUtil.OppositeSideJoint(humanjointType);
            if (jointGameObjects.ContainsKey(jointType))
            {
                if (MoCapDataSource.TryGetMappedJointPosition(jointType, out newJointPosition))
                {
                    Vector3 adjustedPosition = newJointPosition;

                    adjustedPosition = Vector3.Scale(new Vector3(invertMocapX ? -1f : 1f, invertMocapY ? -1f : 1f, invertMocapZ ? -1 : 1), newJointPosition);

                    // Use local position so container rotation is applied to all child joints
                    jointGameObjects[jointType].transform.localPosition = adjustedPosition;
                }
                else
                {
                    //TODO: interpolate position here?
                    Debug.LogWarningFormat("No position mapped to joint '{0}'", jointType.ToString());
                }

            }
        }

        var distanceTolerance = searching ? .1 : .3;
        var distance = Vector3.Distance(jointGameObjects[HumanJointType.HandRight].transform.position, RightHandAnchorObject.transform.position);

        // Rotate all coordinates around the world down vector by the kinect rotation vector to offset kinect rotation
        if (distance > distanceTolerance)
        {
            this.transform.Rotate(Vector3.down, 10, Space.World);
            Debug.Log(distance);
            searching = false;
        }

        if (distance > .5)
        {
            searching = true;
        }
    }

    private float GetXZAngle(Vector3 v1, Vector3 v2)
    {
        v1 = new Vector3(v1.x, 0, v1.z);
        v2 = new Vector3(v2.z, 0, v2.z);

        return Vector3.Angle(v1, v2);
    }

    private void CreateJoints()
    {
        // Transparent joints so looking down doesn't cause blindness
        HumanJointType[] transparentJoints = { HumanJointType.Head, HumanJointType.Neck, HumanJointType.SpineShoulder };

        Debug.LogWarningFormat("Joint Game Objects being Created!!");
        foreach (var jointType in BodyJointPositionMapping.GetAllJointTypes())
        {
            string currentJointName = string.Empty;
            try
            {
                var jointGameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);

                if (transparentJoints.Contains(jointType))
                {
                    // Make transparent joints infinitely small
                    jointGameObject.transform.localScale = new Vector3(0, 0, 0);
                }
                else
                {
                    jointGameObject.transform.localScale = new Vector3(jointSize, jointSize, jointSize);
                }

                jointGameObject.name = jointType.ToString();
                currentJointName = jointType.ToString();
                if (jointType == HumanJointType.HandRight || jointType == HumanJointType.HandTipRight || jointType == HumanJointType.WristRight)
                {
                    var materialColored = new Material(Shader.Find("Diffuse"));
                    materialColored.color = new Color(255, 0, 0);
                    jointGameObject.GetComponent<Renderer>().material = materialColored;
                }
                else
                {
                    jointGameObject.GetComponent<Renderer>().material = jointMaterial;
                }
                jointGameObject.transform.parent = this.transform;
                jointGameObjects.Add(jointType, jointGameObject);
                jointGameObject.AddComponent<Rigidbody>().isKinematic = true;

            }
            catch (Exception ex)
            {
                Debug.LogWarningFormat("Create joint '{0}' Failed. Error: {1}, Message: {2}", currentJointName, ex.GetType().Name, ex.Message);
            }
        }
        JointPositionsLog = new Dictionary<HumanJointType, List<Vector3>>();
    }

    private void CreateBones()
    {
        bones = new List<Bone>();
        // Head - Shoulder
        // Remove neck bone
        //bones.Add(Bone.Create(jointGameObjects[HumanJointType.Head], jointGameObjects[HumanJointType.SpineShoulder]));
        // Sholder - Shoulder Right
        bones.Add(Bone.Create(jointGameObjects[HumanJointType.SpineShoulder], jointGameObjects[HumanJointType.ShoulderRight]));
        // Shoulder Right - Elbow
        bones.Add(Bone.Create(jointGameObjects[HumanJointType.ShoulderRight], jointGameObjects[HumanJointType.ElbowRight]));
        // Elbow Right - Wrist
        bones.Add(Bone.Create(jointGameObjects[HumanJointType.ElbowRight], jointGameObjects[HumanJointType.WristRight]));
        // Wrist Right - Hand
        bones.Add(Bone.Create(jointGameObjects[HumanJointType.WristRight], jointGameObjects[HumanJointType.HandRight]));
        // Wrist Right - Thumb
        bones.Add(Bone.Create(jointGameObjects[HumanJointType.WristRight], jointGameObjects[HumanJointType.ThumbRight]));
        // Shoulder - Shoulder Left
        bones.Add(Bone.Create(jointGameObjects[HumanJointType.SpineShoulder], jointGameObjects[HumanJointType.ShoulderLeft]));
        // Shoulder Left - Elbow
        bones.Add(Bone.Create(jointGameObjects[HumanJointType.ShoulderLeft], jointGameObjects[HumanJointType.ElbowLeft]));
        // Elbow Left - Wrist
        bones.Add(Bone.Create(jointGameObjects[HumanJointType.ElbowLeft], jointGameObjects[HumanJointType.WristLeft]));
        // Wrist Left - Hand
        bones.Add(Bone.Create(jointGameObjects[HumanJointType.WristLeft], jointGameObjects[HumanJointType.HandLeft]));
        // Wrist Left - Thumb
        bones.Add(Bone.Create(jointGameObjects[HumanJointType.WristLeft], jointGameObjects[HumanJointType.ThumbLeft]));
        // Shoulder - Spine Mid
        bones.Add(Bone.Create(jointGameObjects[HumanJointType.SpineShoulder], jointGameObjects[HumanJointType.SpineMid]));
        // Spine Mid - Spine Base
        bones.Add(Bone.Create(jointGameObjects[HumanJointType.SpineMid], jointGameObjects[HumanJointType.SpineBase]));
        // Spine Base - Hip Right
        bones.Add(Bone.Create(jointGameObjects[HumanJointType.SpineBase], jointGameObjects[HumanJointType.HipRight]));
        // Hip Right - Knee
        bones.Add(Bone.Create(jointGameObjects[HumanJointType.HipRight], jointGameObjects[HumanJointType.KneeRight]));
        // Knee Right - Ankle
        bones.Add(Bone.Create(jointGameObjects[HumanJointType.KneeRight], jointGameObjects[HumanJointType.AnkleRight]));
        //Ankle Right - Foot Right
        bones.Add(Bone.Create(jointGameObjects[HumanJointType.AnkleRight], jointGameObjects[HumanJointType.FootRight]));
        // Spine Base - Hip Left
        bones.Add(Bone.Create(jointGameObjects[HumanJointType.SpineBase], jointGameObjects[HumanJointType.HipLeft]));
        // Hip Left - Knee
        bones.Add(Bone.Create(jointGameObjects[HumanJointType.HipLeft], jointGameObjects[HumanJointType.KneeLeft]));
        // Knee Left - Ankle
        bones.Add(Bone.Create(jointGameObjects[HumanJointType.KneeLeft], jointGameObjects[HumanJointType.AnkleLeft]));
        //Ankle Left - Foot Left
        bones.Add(Bone.Create(jointGameObjects[HumanJointType.AnkleLeft], jointGameObjects[HumanJointType.FootLeft]));
    }
}