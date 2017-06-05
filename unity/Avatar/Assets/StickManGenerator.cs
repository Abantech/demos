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
    public float jointSize = .1f;
    public Material jointMaterial;

    private Dictionary<HumanJointType, GameObject> jointGameObjects;
    private List<Bone> bones;
    private bool figureGenerated = false;

    public float hmdHeadPositionX;
    public float hmdHeadPositionY;
    public float hmdHeadPositionZ;

    public float figHeadPositionX;
    public float figHeadPositionY;
    public float figHeadPositionZ;



    //Some Motion Captures do mirror image, adjust these to do the inverse to compensate as required
    public bool invertMocapX;
    public bool invertMocapY;
    public bool invertMocapZ;

    //public Dictionary<HumanJointType, Action<GameObject>> OnJointGeneratedActions;
    //public Dictionary<HumanJointType, Action<GameObject>> OnJointUpdateActions;

    public void Start()
    {
        jointGameObjects = new Dictionary<HumanJointType, GameObject>();
        figHeadPositionX = 0;
        figHeadPositionY = 0;
        figHeadPositionZ = 0;
        //OnJointGeneratedActions = new Dictionary<HumanJointType, Action<GameObject>>();
        //OnJointUpdateActions = new Dictionary<HumanJointType, Action<GameObject>>();
    }

    private BodyJointPositionMapping MoCapDataSource
    {
        get
        {
            return KinectJointPositionMapper.Instance;
        }
    }




    public void Update()
    {
        hmdHeadPositionX = HeadAnchorObject.transform.position.x;
        hmdHeadPositionY = HeadAnchorObject.transform.position.y;
        hmdHeadPositionZ = HeadAnchorObject.transform.position.z;

        if (figureGenerated)
        {
            var headPosition = jointGameObjects[HumanJointType.Head].transform.position;
            MoCapDataSource.Offset = MoCapDataSource.Offset + new Vector3(-MoCapDataSource.HeadPosition.x, -MoCapDataSource.HeadPosition.y, -MoCapDataSource.HeadPosition.z);


            Vector3 mocapCoordinateInversion = new Vector3(invertMocapX ? -1f : 1f, invertMocapY ? -1f : 1f, invertMocapZ ? -1 : 1);

            //Update the joint positions
            foreach (var jointType in BodyJointPositionMapping.GetAllJointTypes())
            {
                Vector3 newJointPosition = Vector3.zero;

                if (jointGameObjects.ContainsKey(jointType))
                {
                    if(MoCapDataSource.TryGetMappedJointPosition(jointType, out newJointPosition))
                    {
                        Vector3 adjustedPosition = Vector3.Scale(newJointPosition, mocapCoordinateInversion);
                        jointGameObjects[jointType].transform.position = adjustedPosition;
                    }
                    else
                    {
                        //TODO: interpolate position here?
                        Debug.LogWarningFormat("No position mapped to joint '{0}'", jointType.ToString());
                    }
                    
                }

                //if (OnJointGeneratedActions.ContainsKey(jointType))
                //{
                //    OnJointGeneratedActions[jointType](jointGameObjects[jointType]);
                //}
            }

            bones.ForEach(bone => Bone.Update(bone));
            figHeadPositionX = jointGameObjects[HumanJointType.Head].transform.position.x;
            figHeadPositionY = jointGameObjects[HumanJointType.Head].transform.position.y;
            figHeadPositionZ = jointGameObjects[HumanJointType.Head].transform.position.z;
            Debug.LogFormat("Avatar Head Position at {0}, {1}, {2}", figHeadPositionX, figHeadPositionY, figHeadPositionZ);
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

    //public void LateUpdate()
    //{
    //    if (figureGenerated)
    //    {
    //        foreach (var bone in bones)
    //        {
    //            Bone.Update(bone);
    //        }
    //    }
    //}

    private void CreateJoints()
    {
        Debug.LogWarningFormat("Joint Game Objects being Created!!");
        foreach (var jointType in BodyJointPositionMapping.GetAllJointTypes())
        {
            string currentJointName = string.Empty;
            try
            {
                var jointGameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                jointGameObject.transform.localScale = new Vector3(jointSize, jointSize, jointSize);
                jointGameObject.name = jointType.ToString();
                currentJointName = jointType.ToString();
                jointGameObject.GetComponent<Renderer>().material = jointMaterial;
                jointGameObject.transform.parent = this.transform;
                jointGameObjects.Add(jointType, jointGameObject);
            }
            catch(Exception ex)
            {
                Debug.LogWarningFormat("Create joint '{0}' Failed. Error: {1}, Message: {2}", currentJointName, ex.GetType().Name, ex.Message);
            }
        }

    }

    private void CreateBones()
    {
        bones = new List<Bone>();
        // Head - Shoulder
        bones.Add(Bone.Create(jointGameObjects[HumanJointType.Head], jointGameObjects[HumanJointType.SpineShoulder]));
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


//[CustomEditor(typeof(StickManGenerator))]
//public class StickManGeneratorEditor : Editor
//{
//    public override void OnInspectorGUI()
//    {
//        StickManGenerator stickManGenerator = (StickManGenerator)target;

//        stickManGenerator.jointTransparency = EditorGUILayout.IntSlider("Transparency", stickManGenerator.jointTransparency, 0, 100);

//        //ProgressBar(stickManGenerator.jointTransparency / 100.0f, "Transparency");
//    }

//    //// Custom GUILayout progress bar.
//    //void ProgressBar(float value, string label)
//    //{
//    //    // Get a rect for the progress bar using the same margins as a textfield:
//    //    Rect rect = GUILayoutUtility.GetRect(18, 18, "TextField");
//    //    EditorGUI.ProgressBar(rect, value, label);
//    //    EditorGUILayout.Space();
//    //}
//}