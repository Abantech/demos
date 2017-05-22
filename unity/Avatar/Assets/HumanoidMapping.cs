using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public abstract class HumanoidMapping : MonoBehaviour, IBodyPartPositionProvider
{
    public bool IsInitialized { get; protected set; }
    public abstract Vector3 ChestPosition { get; set; }
    public abstract Vector3 SpineShoulderPosition { get; set; }
    public abstract Vector3 HeadPosition { get; set; }
    public abstract Vector3 HipsPosition { get; set; }
    public abstract Vector3 LeftArmElbowPosition { get; set; }
    public abstract Vector3 LeftCollarBonePosition { get; set; }
    public abstract Vector3 LeftArmShoulderPosition { get; set; }
    public abstract Vector3 LeftArmWristPosition { get; set; }
    public abstract Vector3 LeftArmHandTipPosition { get; set; }
    public abstract Vector3 LeftArmHandThumbPosition { get; set; }

    //public abstract Vector3 LeftLegHipPosition { get; set; }
    public abstract Vector3 LeftLegAnklePosition { get; set; }
    public abstract Vector3 LeftLegKneePosition { get; set; }
    public abstract Vector3 LeftLegToesPosition { get; set; }
    public abstract Vector3 LeftLegHipPosition { get; set; }
    public abstract Vector3 NeckPosition { get; set; }
    public abstract Vector3 RightArmElbowPosition { get; set; }
    public abstract Vector3 RightCollarBonePosition { get; set; }
    public abstract Vector3 RightArmShoulderPosition { get; set; }
    public abstract Vector3 RightArmWristPosition { get; set; }

    public abstract Vector3 RightArmHandTipPosition { get; set; }
    public abstract Vector3 RightArmHandThumbPosition { get; set; }

    //public abstract Vector3 RightLegHipPosition { get; set; }
    public abstract Vector3 RightLegAnklePosition { get; set; }
    public abstract Vector3 RightLegKneePosition { get; set; }
    public abstract Vector3 RightLegFootPosition { get; set; }
    public abstract Vector3 RightLegHipPosition { get; set; }
    public abstract Vector3 SpinePosition { get; set; }
}
