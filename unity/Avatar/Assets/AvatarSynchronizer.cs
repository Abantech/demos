using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class AvatarSynchronizer : MonoBehaviour
{
    //Source 
    public HumanoidMapping MocapDataSource;
    public HumanoidMapping AvatarTarget;

    private void Start()
    {
    }

    private void Update()
    {
        if (MocapDataSource.IsInitialized && AvatarTarget.IsInitialized)
        {
            AvatarTarget.ChestPosition = MocapDataSource.ChestPosition;
            AvatarTarget.SpineShoulderPosition = MocapDataSource.SpineShoulderPosition;
            AvatarTarget.HeadPosition = MocapDataSource.HeadPosition;
            AvatarTarget.HipsPosition = MocapDataSource.HipsPosition;

            AvatarTarget.LeftArmElbowPosition = MocapDataSource.LeftArmElbowPosition;
            AvatarTarget.LeftCollarBonePosition = MocapDataSource.LeftCollarBonePosition;
            AvatarTarget.LeftArmShoulderPosition = MocapDataSource.LeftArmShoulderPosition;
            AvatarTarget.LeftArmWristPosition = MocapDataSource.LeftArmWristPosition;
            AvatarTarget.LeftArmHandTipPosition = MocapDataSource.LeftArmHandTipPosition;
            AvatarTarget.LeftArmHandThumbPosition = MocapDataSource.LeftArmHandThumbPosition;


            AvatarTarget.LeftLegAnklePosition = MocapDataSource.LeftLegAnklePosition;
            AvatarTarget.LeftLegKneePosition = MocapDataSource.LeftLegKneePosition;
            AvatarTarget.LeftLegToesPosition = MocapDataSource.LeftLegToesPosition;
            AvatarTarget.LeftLegHipPosition = MocapDataSource.LeftLegHipPosition;

            AvatarTarget.NeckPosition = MocapDataSource.NeckPosition;

            AvatarTarget.RightArmElbowPosition = MocapDataSource.RightArmElbowPosition;
            AvatarTarget.RightCollarBonePosition = MocapDataSource.RightCollarBonePosition;
            AvatarTarget.RightArmShoulderPosition = MocapDataSource.RightArmShoulderPosition;
            AvatarTarget.RightArmWristPosition = MocapDataSource.RightArmWristPosition;
            AvatarTarget.RightArmHandTipPosition = MocapDataSource.RightArmHandTipPosition;
            AvatarTarget.RightArmHandThumbPosition = MocapDataSource.RightArmHandThumbPosition;

            AvatarTarget.RightLegAnklePosition = MocapDataSource.RightLegAnklePosition;
            AvatarTarget.RightLegKneePosition = MocapDataSource.RightLegKneePosition;
            AvatarTarget.RightLegFootPosition = MocapDataSource.RightLegFootPosition;
            AvatarTarget.RightLegHipPosition = MocapDataSource.RightLegHipPosition;
            AvatarTarget.SpinePosition = MocapDataSource.SpinePosition;
        }
    }
}