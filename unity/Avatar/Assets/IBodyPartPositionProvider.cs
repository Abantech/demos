using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

    public interface IBodyPartPositionProvider
    {
        Vector3 HeadPosition  { get; set; }
        Vector3 NeckPosition  { get; set; }
    
        Vector3 HipsPosition  { get; set; }
        Vector3 SpinePosition  { get; set; } 
        Vector3 ChestPosition  { get; set; }
        Vector3 RightCollarBonePosition  { get; set; }
        Vector3 RightArmShoulderPosition  { get; set; }
        Vector3 RightArmElbowPosition  { get; set; }
        Vector3 RightArmWristPosition  { get; set; }

        Vector3 RightLegHipPosition  { get; set; }
        Vector3 RightLegKneePosition  { get; set; }
        Vector3 RightLegAnklePosition  { get; set; }

        Vector3 RightLegFootPosition  { get; set; }

        Vector3 LeftCollarBonePosition  { get; set; }
        Vector3 LeftArmShoulderPosition  { get; set; }
        Vector3 LeftArmElbowPosition  { get; set; }
        Vector3 LeftArmWristPosition  { get; set; }

        Vector3 LeftLegHipPosition  { get; set; }
        Vector3 LeftLegKneePosition  { get; set; }
        Vector3 LeftLegAnklePosition  { get; set; }
        Vector3 LeftLegToesPosition  { get; set; }

        //TO DO: Finger joints

    }