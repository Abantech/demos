using System.Runtime.Serialization;

namespace HandJointsMeasurement
{
    /// <summary>
    /// An enumeration representing the different fingers
    /// </summary>
    [DataContract]
    public enum FingerEnum
    {
        /// <summary>
        /// The thumb
        /// </summary>
        [EnumMember]
        Thumb,

        /// <summary>
        /// The index finger
        /// </summary>
        [EnumMember]
        Index,

        /// <summary>
        /// The middle finger
        /// </summary>
        [EnumMember]
        Middle,

        /// <summary>
        /// The ring finger
        /// </summary>
        [EnumMember]
        Ring,

        /// <summary>
        /// The pinky finger
        /// </summary>
        [EnumMember]
        Pinky,

        [EnumMember]
        Unknown
    }
}
