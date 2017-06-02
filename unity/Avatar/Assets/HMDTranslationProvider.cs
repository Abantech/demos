using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class HMDTranslationProvider : BaseTransformProvider, IViewPortTransformProvider
{
    private bool calibratePosition = false;
    private bool calibrateRotation = false;

    public override bool CalibratePosition
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

    public override bool CalibrateRotation
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
}

