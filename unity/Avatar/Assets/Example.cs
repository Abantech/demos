using UnityEngine;

namespace Assets
{
    class Example
    {
        Vector3 HeadFromKinect;
        Vector3 LeftHandFromKinect;
        Vector3 RightHandFromKinect;

        Vector3 HeadFromVive;
        Vector3 LeftHandFromLeap;
        Vector3 RightHandFromLeap;

        public void ViveHeadAsCenter()
        {
            // Get translation correction. Can be vector
            float xCorrection = HeadFromVive.x - HeadFromKinect.x;
            float yCorrection = HeadFromVive.y - HeadFromKinect.y;
            float zCorrection = HeadFromVive.z - HeadFromKinect.z;

            // Apply distances to each point
            HeadFromKinect = new Vector3(HeadFromKinect.x + xCorrection, HeadFromKinect.y + yCorrection, HeadFromKinect.z + zCorrection);
            LeftHandFromKinect = new Vector3(LeftHandFromKinect.x + xCorrection, LeftHandFromKinect.y + yCorrection, LeftHandFromKinect.z + zCorrection);
            RightHandFromKinect = new Vector3(RightHandFromKinect.x + xCorrection, RightHandFromKinect.y + yCorrection, RightHandFromKinect.z + zCorrection);

            // Find Vectors to match
            var HeadToHandKinect = HeadFromKinect - LeftHandFromKinect;
            var HeadToHandUser = HeadFromVive - LeftHandFromLeap;

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
            exampleObject.transform.RotateAround(HeadFromVive, Vector3.right, xRotation);
            exampleObject.transform.RotateAround(HeadFromVive, Vector3.up, yRotation);
            exampleObject.transform.RotateAround(HeadFromVive, Vector3.forward, zRotation);
        }
    }
}
