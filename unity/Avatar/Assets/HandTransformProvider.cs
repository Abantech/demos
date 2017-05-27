using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// Provides access to the transform that will be used as the source or target for translation and/or rotation
/// </summary>
public abstract class HandTransformProvider : BodyJointPositionMapping, IHandTransformProvider
{
    public Transform MappedTransform { get; set; }
    public Action<Transform> CalibrationFunction { get; set; }
    public bool IsCalibrated { get; set; }
    public bool CalibratePosition { get; set; }
    public bool CalibrateRotation { get; set; }

    public HumanJointType HandType { get; set; }

    public void ApplyCalibration()
    {
        CalibrationFunction(MappedTransform);
    }
}