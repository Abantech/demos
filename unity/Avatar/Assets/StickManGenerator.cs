using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

public class StickManGenerator : AvatarGenerator
{
    public BodyJointPositionMapping MoCapDataSource;
    public GameObject HeadAnchorObject;
    public UnityEngine.Color jointColor = UnityEngine.Color.blue;
    [Range(0.0f, 1.0f)]
    public float jointTransparency = 0.5f;
    public float jointSize = .1f;

    private Dictionary<HumanJointType, GameObject> jointGameObjects;
    private List<Bone> bones;
    private bool figureGenerated = false;
    //public Dictionary<HumanJointType, Action<GameObject>> OnJointGeneratedActions;
    //public Dictionary<HumanJointType, Action<GameObject>> OnJointUpdateActions;

    public void Start()
    {
        jointGameObjects = new Dictionary<HumanJointType, GameObject>();
        //OnJointGeneratedActions = new Dictionary<HumanJointType, Action<GameObject>>();
        //OnJointUpdateActions = new Dictionary<HumanJointType, Action<GameObject>>();
    }

    public void Update()
    {
        if(figureGenerated)
        {
            //var headPosition = jointGameObjects[HumanJointType.Head].transform.position;
            MoCapDataSource.Offset = MoCapDataSource.Offset + new Vector3(-MoCapDataSource.HeadPosition.x, -MoCapDataSource.HeadPosition.y, -MoCapDataSource.HeadPosition.z);

            //Update the joint positions
            foreach (var jointType in BodyJointPositionMapping.GetAllJointTypes())
            {
                Vector3 newJointPosition = Vector3.zero;

                if (jointGameObjects.ContainsKey(jointType))
                {
                    if(MoCapDataSource.TryGetMappedJointPosition(jointType, out newJointPosition))
                    {
                        jointGameObjects[jointType].transform.position = newJointPosition;
                        jointGameObjects[jointType].GetComponent<Renderer>().material.color = new Color(jointColor.r, jointColor.g, jointColor.b, jointTransparency);
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

            foreach (var bone in bones)
            {
                Bone.Update(bone);
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
                    materialColored.color = new Color(jointColor.r, jointColor.g, jointColor.b, jointTransparency);
                    jointGameObject.GetComponent<Renderer>().material = materialColored;
                    jointGameObjects.Add(jointType, jointGameObject);
                    jointGameObject.transform.parent = this.transform;
                }

                //if (HeadAnchorObject != null)
                //{
                //    jointGameObjects[HumanJointType.Head].transform.parent = HeadAnchorObject.transform;
                //}

                //foreach (var jointType in BodyJointPositionMapping.GetAllJointTypes())
                //{
                //    //TODO
                //    if (OnJointGeneratedActions.ContainsKey(jointType))
                //    {
                //        OnJointGeneratedActions[jointType](jointGameObjects[jointType]);
                //    }
                //}

                 bones = new List<Bone>();

                CreateBones();
                foreach (var bone in bones)
                {
                    bone.GetBoneGameObject().transform.parent = this.transform;
                }

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