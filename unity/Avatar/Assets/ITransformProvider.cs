using System;
using UnityEngine;

/// <summary>
/// Behavior to attach to a gameobject that can be the source or target of calibration
/// </summary>
interface ITransformProvider
{
    Transform MappedTransform { get; set; }
    Action<Transform> CalibrationFunction { get; set; }

    /// <summary>
    /// Gets or sets whether or not the required calibrations have been applied.
    /// </summary>
    bool IsCalibrated { get; set; }

    /// <summary>
    /// Gets or sets whether or not the transform position requires calibration
    /// </summary>
    bool CalibratePosition { get; set; }

    /// <summary>
    /// Gets or sets whether or not the transform rotation requires calibration
    /// </summary>
    bool CalibrateRotation { get; set; }

    void ApplyCalibration();
}