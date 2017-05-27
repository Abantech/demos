using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class HMDTranslationProvider : BaseTransformProvider, IViewPortTransformProvider
{
    private bool calibratePosition = false;
    private bool calibrateRotation = false;

    public bool CalibratePosition
    {
        get
        {
            return calibratePosition;
        }

        set
        {
            calibratePosition = value;
        }
    }

    public bool CalibrateRotation
    {
        get
        {
            return calibrateRotation;
        }

        set
        {
            calibrateRotation = value;
        }
    }

    public Action<Transform> CalibrationFunction { get; set; }

    public bool IsCalibrated { get; set; }

    public Transform MappedTransform { get; set; }

    public void Start()
    {
        this.MappedTransform = this.transform;
    }

    public void ApplyCalibration()
    {
        if((CalibratePosition || CalibrateRotation) && CalibrationFunction != null)
        {
            CalibrationFunction(MappedTransform);
        }
    }
}

