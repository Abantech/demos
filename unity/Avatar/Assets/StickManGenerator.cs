using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class StickManGenerator : MonoBehaviour
{
    public BodyJointPositionMapping MoCapDataSource;

    private Dictionary<HumanJointType, GameObject> jointGameObjects;
    private List<Bone> bones;
    public UnityEngine.Color jointColor = UnityEngine.Color.blue;
    public float jointSize = .1f;

    private bool figureGenerated = false;
    public Dictionary<HumanJointType, Action<GameObject>> OnJointGeneratedActions;
    public Dictionary<HumanJointType, Action<GameObject>> OnJointUpdateActions;

    public void Start()
    {
        OnJointGeneratedActions = new Dictionary<HumanJointType, Action<GameObject>>();
        OnJointUpdateActions = new Dictionary<HumanJointType, Action<GameObject>>();
    }

    public void Update()
    {
        if(figureGenerated)
        {
            //Update the joint positions
            foreach (var jointType in BodyJointPositionMapping.GetAllJointTypes())
            {
                jointGameObjects[jointType].transform.position = MoCapDataSource.GetMappedJointPosition(jointType);

                if (OnJointGeneratedActions.ContainsKey(jointType))
                {
                    OnJointGeneratedActions[jointType](jointGameObjects[jointType]);
                }
            }
        }
        else
        {
            if (MoCapDataSource.IsInitialized)
            {
                //Create joints and bones for the first time
                foreach (var jointType in BodyJointPositionMapping.GetAllJointTypes())
                {
                    var jointGameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    jointGameObject.transform.localScale = new Vector3(jointSize, jointSize, jointSize);
                    jointGameObject.name = jointType.ToString();

                    //create a new material
                    var materialColored = new Material(Shader.Find("Diffuse"));
                    materialColored.color = jointColor;
                    jointGameObject.GetComponent<Renderer>().material = materialColored;
                    jointGameObjects.Add(jointType, jointGameObject);
                }

                foreach (var jointType in BodyJointPositionMapping.GetAllJointTypes())
                {
                    //TODO
                    if (OnJointGeneratedActions.ContainsKey(jointType))
                    {
                        OnJointGeneratedActions[jointType](jointGameObjects[jointType]);
                    }
                }

                 bones = new List<Bone>();

                CreateBones();

                figureGenerated = true;
            }
        }
    }

    public void LateUpdate()
    {
        if (figureGenerated)
        {
            foreach (var bone in bones)
            {
                Bone.Update(bone);
            }
        }
    }

    private void CreateBones()
    {
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
