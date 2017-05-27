using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// Provides access to the transform that will be used as the source or target for translation and/or rotation
/// </summary>
public abstract class HandTransformProvider : BaseTransformProvider, IHandTransformProvider
{
    public abstract HumanJointType GetHandType();

}