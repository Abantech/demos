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
        Vector3 RightArmShoulderPosition  { get; set; }
        Vector3 RightArmUpperPosition  { get; set; }
        Vector3 RightArmLowerPosition  { get; set; }
        Vector3 RightArmWristPosition  { get; set; }

        Vector3 RightLegUpperPosition  { get; set; }
        Vector3 RightLegLowerPosition  { get; set; }
        Vector3 RightLegFootPosition  { get; set; }

        Vector3 RightLegToesPosition  { get; set; }

        Vector3 LeftArmShoulderPosition  { get; set; }
        Vector3 LeftArmUpperPosition  { get; set; }
        Vector3 LeftArmLowerPosition  { get; set; }
        Vector3 LeftArmWristPosition  { get; set; }

        Vector3 LeftLegUpperPosition  { get; set; }
        Vector3 LeftLegLowerPosition  { get; set; }
        Vector3 LeftLegFootPosition  { get; set; }
        Vector3 LeftLegToesPosition  { get; set; }

        //TO DO: Finger joints

    }