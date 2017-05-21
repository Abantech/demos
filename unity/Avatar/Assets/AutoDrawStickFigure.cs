using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AutoDrawStickFigure : MonoBehaviour
{
    ConcurrentDictionary<ulong, KinectAutoStickMan> bodies = new ConcurrentDictionary<ulong, KinectAutoStickMan>();

    public bool HasBodyData { get { return LocalKinectController.HasBodyData(); } }

    void Update()
    {
        if (LocalKinectController.HasBodyData())
        {
            // Get Tracked Bodies
            var trackedBodies = LocalKinectController.GetTrackedBodies();

            // Track bodies in the frame
            List<ulong> trackedBodiesIDsThisFrame = new List<ulong>();

            // Loop through tracked bodies
            foreach (var detectedBody in trackedBodies)
            {
                // Check if dictionary already contains body
                if (!bodies.ContainsKey(detectedBody.TrackingId))
                {
                    // Create new body
                    bodies.Add(detectedBody.TrackingId, new KinectAutoStickMan(detectedBody));
                }

                // Update joints
                bodies[detectedBody.TrackingId].UpdateJoints(detectedBody.Joints.Values);

                // Add body id to tracked
                trackedBodiesIDsThisFrame.Add(detectedBody.TrackingId);
            }

            foreach (var key in bodies.GetKeysArray())
            {
                // Find old bodies in the dictionary
                if (!trackedBodiesIDsThisFrame.Contains(key))
                {
                    // Clear them
                    bodies[key].Clear();

                    // Remove them from the dictionary
                    bodies.Remove(key);
                }
            }
        }
        else
        {
            foreach (var key in bodies.GetKeysArray())
            {
                bodies[key].Clear();
            }

            bodies = new ConcurrentDictionary<ulong, KinectAutoStickMan>();
        }
    }
}
