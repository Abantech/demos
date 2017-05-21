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

            AvatarTarget.LeftArmLowerPosition = MocapDataSource.LeftArmLowerPosition;
            AvatarTarget.LeftArmShoulderPosition = MocapDataSource.LeftArmShoulderPosition;
            AvatarTarget.LeftArmUpperPosition = MocapDataSource.LeftArmUpperPosition;
            AvatarTarget.LeftArmWristPosition = MocapDataSource.LeftArmWristPosition;
            AvatarTarget.LeftArmHandTipPosition = MocapDataSource.LeftArmHandTipPosition;
            AvatarTarget.LeftArmHandThumbPosition = MocapDataSource.LeftArmHandThumbPosition;


            AvatarTarget.LeftLegFootPosition = MocapDataSource.LeftLegFootPosition;
            AvatarTarget.LeftLegLowerPosition = MocapDataSource.LeftLegLowerPosition;
            AvatarTarget.LeftLegToesPosition = MocapDataSource.LeftLegToesPosition;
            AvatarTarget.LeftLegUpperPosition = MocapDataSource.LeftLegUpperPosition;

            AvatarTarget.NeckPosition = MocapDataSource.NeckPosition;

            AvatarTarget.RightArmLowerPosition = MocapDataSource.RightArmLowerPosition;
            AvatarTarget.RightArmShoulderPosition = MocapDataSource.RightArmShoulderPosition;
            AvatarTarget.RightArmUpperPosition = MocapDataSource.RightArmUpperPosition;
            AvatarTarget.RightArmWristPosition = MocapDataSource.RightArmWristPosition;
            AvatarTarget.RightArmHandTipPosition = MocapDataSource.RightArmHandTipPosition;
            AvatarTarget.RightArmHandThumbPosition = MocapDataSource.RightArmHandThumbPosition;

            AvatarTarget.RightLegFootPosition = MocapDataSource.RightLegFootPosition;
            AvatarTarget.RightLegLowerPosition = MocapDataSource.RightLegLowerPosition;
            AvatarTarget.RightLegToesPosition = MocapDataSource.RightLegToesPosition;
            AvatarTarget.RightLegUpperPosition = MocapDataSource.RightLegUpperPosition;
            AvatarTarget.SpinePosition = MocapDataSource.SpinePosition;
        }
    }
}