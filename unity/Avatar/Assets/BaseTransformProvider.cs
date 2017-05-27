using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public abstract class BaseTransformProvider : MonoBehaviour, ITransformProvider
{
    public virtual bool CalibratePosition { get; set; }

    public virtual bool CalibrateRotation { get; set; }

    public virtual Action<Transform> CalibrationFunction { get; set; }

    public virtual bool IsCalibrated { get; set; }
    public virtual Transform MappedTransform { get; set; }

    public void Start()
    {
        this.MappedTransform = this.transform;
    }

    public virtual void ApplyCalibration()
    {
        if ((CalibratePosition || CalibrateRotation) && CalibrationFunction != null)
        {
            CalibrationFunction(MappedTransform);
        }
    }
}
