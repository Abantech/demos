using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Windows.Kinect;

public class KinectJointPositionMapper : BodyJointPositionMapping //MonoBehaviour, IBodyPartPositionProvider
{
    Dictionary<JointType, Vector3> LastKnownJointPositions = new Dictionary<JointType, Vector3>();
    ulong trackedBodyId;
    bool hasTrackedBody = false;
    private Windows.Kinect.Body trackedBody;

    private void Start()
    {
        
    }

    private void Update()
    {
        this.IsInitialized = LocalKinectController.IsInitialized;

        if (this.IsInitialized)
        {
            if (LocalKinectController.HasBodyData())
            {
                // Get Tracked Bodies
                var trackedBodies = LocalKinectController.GetTrackedBodies();

                if (!hasTrackedBody)
                {
                    hasTrackedBody = true;
                    trackedBodyId = trackedBodies.First().TrackingId;
                }
                else
                {
                    //TODO: Make sure it's the same tracked body as before. If not, do we stop updating the positions?
                }
            }
            else
            {
                if (hasTrackedBody)
                {
                    //TODO: clear the skeleton here
                }
            }

            if (hasTrackedBody)
            {
                var trackedBodies = LocalKinectController.GetTrackedBodies();
                //Get the tracked body we've originally captured

                //Note this can get expensive - figure out a way to avoid reassigning this all the time?
                trackedBody = trackedBodies.Single(x => x.TrackingId == trackedBodyId);
            }
        }
    }

    private Vector3 GetJointType(HumanJointType jointType)
    {
        int jointTypeIndex = (int)jointType;
        JointType kinectJointType = ((JointType)jointTypeIndex);
        return GetJointPosition(kinectJointType);
    }

    private Vector3 GetJointPosition(JointType jointType)
    {
        
        Vector3 derivedVector = Vector3.zero;

        if (hasTrackedBody)
        {
            if (trackedBody.Joints.ContainsKey(jointType))
            {
                derivedVector = new Vector3(trackedBody.Joints[jointType].Position.X, trackedBody.Joints[jointType].Position.Y, trackedBody.Joints[jointType].Position.Z);
                if (!LastKnownJointPositions.ContainsKey(jointType))
                {
                    LastKnownJointPositions.Add(jointType, derivedVector);
                }
                else
                {
                    LastKnownJointPositions[jointType] = derivedVector;
                }
            }
            else
            {
                Debug.LogWarning("Kinect does not have tracked joint of type " + jointType.ToString());
            }
        }
        else if (LastKnownJointPositions.ContainsKey(jointType))
        {
            derivedVector = LastKnownJointPositions[jointType];
        }


        return derivedVector;
    }

    public override Vector3 HeadPosition
    {
        get
        {
            return GetJointPosition(JointType.Head);
        }

        set
        {
            throw new NotImplementedException();
        }
    }


    public override Vector3 NeckPosition
    {
        get
        {
            return GetJointPosition(JointType.Neck);
        }

        set
        {
            throw new NotImplementedException();
        }
    }
    public override Vector3 SpineShoulderPosition
    {
        get
        {
            return GetJointPosition(JointType.SpineShoulder);
        }

        set
        {
            throw new NotImplementedException();
        }
    }

    public override Vector3 SpineMidPosition
    {
        get
        {
            return GetJointPosition(JointType.SpineMid);
        }

        set
        {
            throw new NotImplementedException();
        }
    }

    public override Vector3 SpineBasePosition
    {
        get
        {
            return GetJointPosition(JointType.SpineBase);
        }

        set
        {
            throw new NotImplementedException();
        }
    }

    public override Vector3 RightArmShoulderPosition
    {
        get
        {
            return GetJointPosition(JointType.ShoulderRight);
        }

        set
        {
            throw new NotImplementedException();
        }
    }
    public override Vector3 RightArmElbowPosition
    {
        get
        {
            return GetJointPosition(JointType.ElbowRight);
        }

        set
        {
            throw new NotImplementedException();
        }
    }
    public override Vector3 RightArmWristPosition
    {
        get
        {
            return GetJointPosition(JointType.WristRight);
        }

        set
        {
            throw new NotImplementedException();
        }
    }

    public override Vector3 RightArmHandPosition
    {
        get
        {
            return GetJointPosition(JointType.HandRight);
        }

        set
        {
            throw new NotImplementedException();
        }
    }

    public override Vector3 RightArmHandTipPosition
    {
        get
        {
            return GetJointPosition(JointType.HandTipRight);
        }

        set
        {
            throw new NotImplementedException();
        }
    }

    public override Vector3 RightArmHandThumbPosition
    {
        get
        {
            return GetJointPosition(JointType.ThumbRight);
        }

        set
        {
            throw new NotImplementedException();
        }
    }

    public override Vector3 RightLegHipPosition
    {
        get
        {
            return GetJointPosition(JointType.HipRight);
        }

        set
        {
            throw new NotImplementedException();
        }
    }
    public override Vector3 RightLegKneePosition
    {
        get
        {
            return GetJointPosition(JointType.KneeRight);
        }

        set
        {
            throw new NotImplementedException();
        }
    }
    public override Vector3 RightLegAnklePosition
    {
        get
        {
            return GetJointPosition(JointType.AnkleRight);
        }

        set
        {
            throw new NotImplementedException();
        }
    }

    public override Vector3 RightLegFootPosition
    {
        get
        {
            return GetJointPosition(JointType.FootRight);
        }

        set
        {
            throw new NotImplementedException();
        }
    }

    public override Vector3 LeftArmShoulderPosition
    {
        get
        {
            return GetJointPosition(JointType.ShoulderLeft);
        }

        set
        {
            throw new NotImplementedException();
        }
    }
    public override Vector3 LeftArmElbowPosition
    {
        get
        {
            return GetJointPosition(JointType.ElbowLeft);
        }

        set
        {
            throw new NotImplementedException();
        }
    }
    public override Vector3 LeftArmWristPosition
    {
        get
        {
            return GetJointPosition(JointType.WristLeft);
        }

        set
        {
            throw new NotImplementedException();
        }
    }

    public override Vector3 LeftArmHandPosition
    {
        get
        {
            return GetJointPosition(JointType.HandLeft);
        }

        set
        {
            throw new NotImplementedException();
        }
    }

    public override Vector3 LeftArmHandTipPosition
    {
        get
        {
            return GetJointPosition(JointType.HandTipLeft);
        }

        set
        {
            throw new NotImplementedException();
        }
    }

    public override Vector3 LeftArmHandThumbPosition
    {
        get
        {
            return GetJointPosition(JointType.ThumbLeft);
        }

        set
        {
            throw new NotImplementedException();
        }
    }

    public override Vector3 LeftLegHipPosition
    {
        get
        {
            return GetJointPosition(JointType.HipLeft);
        }

        set
        {
            throw new NotImplementedException();
        }
    }
    public override Vector3 LeftLegKneePosition
    {
        get
        {
            return GetJointPosition(JointType.KneeLeft);
        }

        set
        {
            throw new NotImplementedException();
        }
    }
    public override Vector3 LeftLegAnklePosition
    {
        get
        {
            return GetJointPosition(JointType.AnkleLeft);
        }

        set
        {
            throw new NotImplementedException();
        }
    }

    public override Vector3 LeftLegFootPosition
    {
        get
        {
            return GetJointPosition(JointType.FootLeft);
        }

        set
        {
            throw new NotImplementedException();
        }
    }
}

