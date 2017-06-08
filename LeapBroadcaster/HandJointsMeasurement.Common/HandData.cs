using System.Runtime.Serialization;

namespace HandJointsMeasurement
{
    /// <summary>
    /// Data about the hand
    /// </summary>
    [DataContract]
    public class HandData
    {
        [DataMember]
        public Hand LeftHand { get; set; }

        [DataMember]
        public Hand RightHand { get; set; }
    }
}