using UnityEngine;
using System.Collections;
using UnityEditor;

public class DetectWithinSphere : MonoBehaviour
{
    public float sphereRadius;

    void Start()
    {
    }

    void WarningNoise()
    {
        // Play a noise if an object is within the sphere's radius.
        if (Physics.CheckSphere(transform.position, sphereRadius))
        {
            EditorApplication.Beep();
        }
    }
}