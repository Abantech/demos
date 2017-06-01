using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// Definines the inputs and the translations to be applied to either head-centric orientation or joint collection directed orientation
/// </summary>
public class FPVAdjuster
{
    public LeftHandTranslationProvider LeftHandFromLeap;
    public RightHandTranslationProvider RightHandFromLeap;
    public HMDTranslationProvider HeadFromVive;

    public GenericTransformProvider HeadFromKinect;
    public GenericTransformProvider LeftHandFromKinect;
    public GenericTransformProvider RightHandFromKinect;

    public void AdjustKinectJointsAroundVive()
    {
        // Get translation correction. Can be vector
        float xCorrection = HeadFromVive.MappedTransform.position.x - HeadFromKinect.MappedTransform.position.x;
        float yCorrection = HeadFromVive.MappedTransform.position.y - HeadFromKinect.MappedTransform.position.y;
        float zCorrection = HeadFromVive.MappedTransform.position.z - HeadFromKinect.MappedTransform.position.z;

        // Apply distances to each point
        
        HeadFromKinect.MappedTransform.position = new Vector3(HeadFromKinect.MappedTransform.position.x + xCorrection, HeadFromKinect.MappedTransform.position.y + yCorrection, HeadFromKinect.MappedTransform.position.z + zCorrection);
        LeftHandFromKinect.MappedTransform.position = new Vector3(LeftHandFromKinect.MappedTransform.position.x + xCorrection, LeftHandFromKinect.MappedTransform.position.y + yCorrection, LeftHandFromKinect.MappedTransform.position.z + zCorrection);
        RightHandFromKinect.MappedTransform.position = new Vector3(RightHandFromKinect.MappedTransform.position.x + xCorrection, RightHandFromKinect.MappedTransform.position.y + yCorrection, RightHandFromKinect.MappedTransform.position.z + zCorrection);

        // Find Vectors to match
        var HeadToHandKinect = HeadFromKinect.MappedTransform.position - LeftHandFromKinect.MappedTransform.position;
        var HeadToHandUser = HeadFromVive.MappedTransform.position - LeftHandFromLeap.MappedTransform.position;

        // Get rotation correction on x
        Vector3 HeadToHandKinectX = new Vector3(HeadToHandKinect.x, 0, 0);
        Vector3 HeadToHandUserX = new Vector3(HeadToHandUser.x, 0, 0);
        float xRotation = Vector3.Angle(HeadToHandKinectX, HeadToHandUserX);

        // Get rotation correction on y
        Vector3 HeadToHandKinectY = new Vector3(0, HeadToHandKinect.y, 0);
        Vector3 HeadToHandUserY = new Vector3(0, HeadToHandUser.y, 0);
        float yRotation = Vector3.Angle(HeadToHandKinectX, HeadToHandUserX);

        // Get rotation correction on z
        Vector3 HeadToHandKinectZ = new Vector3(0, 0, HeadToHandKinect.z);
        Vector3 HeadToHandUserZ = new Vector3(0, 0, HeadToHandUser.z);
        float zRotation = Vector3.Angle(HeadToHandKinectX, HeadToHandUserX);

        // Transform points around head by rotations
        GameObject exampleObject = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        exampleObject.transform.RotateAround(HeadFromVive.MappedTransform.position, Vector3.right, xRotation);
        exampleObject.transform.RotateAround(HeadFromVive.MappedTransform.position, Vector3.up, yRotation);
        exampleObject.transform.RotateAround(HeadFromVive.MappedTransform.position, Vector3.forward, zRotation);
    }
}
