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
    public bool flipHorizontal = true;

    private void Start()
    {
        Offset = Vector3.zero;
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

                if (trackedBodies != null && trackedBodies.Length > 0)
                {
                    //Note this can get expensive - figure out a way to avoid reassigning this all the time?
                    trackedBody = trackedBodies.Single(x => x.TrackingId == trackedBodyId);
                }
                
            }
        }
    }

    public Vector3 GetJointPosition(HumanJointType jointType)
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

        return derivedVector + Offset;
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
            return flipHorizontal ? GetJointPosition(JointType.ShoulderRight) : GetJointPosition(JointType.ShoulderLeft);
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
            return flipHorizontal ? GetJointPosition(JointType.ElbowRight) : GetJointPosition(JointType.ElbowLeft);
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
            return flipHorizontal ? GetJointPosition(JointType.WristRight) : GetJointPosition(JointType.WristLeft);
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
            return flipHorizontal ? GetJointPosition(JointType.HandRight) : GetJointPosition(JointType.HandLeft);
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
            return flipHorizontal ? GetJointPosition(JointType.HandTipRight) : GetJointPosition(JointType.HandTipLeft);
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
            return flipHorizontal ? GetJointPosition(JointType.ThumbRight) : GetJointPosition(JointType.ThumbLeft);
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
            return flipHorizontal ? GetJointPosition(JointType.HipRight) : GetJointPosition(JointType.HipLeft);
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
            return flipHorizontal ? GetJointPosition(JointType.KneeRight) : GetJointPosition(JointType.KneeLeft);
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
            return flipHorizontal ? GetJointPosition(JointType.AnkleRight) : GetJointPosition(JointType.AnkleLeft);
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
            return flipHorizontal ? GetJointPosition(JointType.FootRight) : GetJointPosition(JointType.FootLeft);
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
            return flipHorizontal ? GetJointPosition(JointType.ShoulderLeft) : GetJointPosition(JointType.ShoulderRight);
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
            return flipHorizontal ? GetJointPosition(JointType.ElbowLeft) : GetJointPosition(JointType.ElbowRight);
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
            return flipHorizontal ? GetJointPosition(JointType.WristLeft) : GetJointPosition(JointType.WristRight);
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
            return flipHorizontal ? GetJointPosition(JointType.HandLeft) : GetJointPosition(JointType.HandRight);
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
            return flipHorizontal ? GetJointPosition(JointType.HandTipLeft) : GetJointPosition(JointType.HandTipRight);
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
            return flipHorizontal ? GetJointPosition(JointType.ThumbLeft) : GetJointPosition(JointType.ThumbRight);
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
            return flipHorizontal ? GetJointPosition(JointType.HipLeft) : GetJointPosition(JointType.HipRight);
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
            return flipHorizontal ? GetJointPosition(JointType.KneeLeft) : GetJointPosition(JointType.KneeRight);
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
            return flipHorizontal ? GetJointPosition(JointType.AnkleLeft) : GetJointPosition(JointType.AnkleRight);
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
            return flipHorizontal ? GetJointPosition(JointType.FootLeft) : GetJointPosition(JointType.FootRight);
        }

        set
        {
            throw new NotImplementedException();
        }
    }
}

