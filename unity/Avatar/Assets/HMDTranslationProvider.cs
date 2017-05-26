﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class HMDTranslationProvider : MonoBehaviour, IViewPortTransformProvider
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

    public Action<Transform> CalibrationFunction
    {
        get
        {
            throw new NotImplementedException();
        }

        set
        {
            throw new NotImplementedException();
        }
    }

    public bool IsCalibrated
    {
        get
        {
            throw new NotImplementedException();
        }

        set
        {
            throw new NotImplementedException();
        }
    }

    public Transform MappedTransform
    {
        get
        {
            throw new NotImplementedException();
        }

        set
        {
            throw new NotImplementedException();
        }
    }

    public void ApplyCalibration()
    {
        throw new NotImplementedException();
    }
}
