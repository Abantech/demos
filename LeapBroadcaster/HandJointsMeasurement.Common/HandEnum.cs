using System.Runtime.Serialization;

namespace HandJointsMeasurement
{
    /// <summary>
    /// An enumeration representing the different hands
    /// </summary>
    [DataContract]
    public enum HandEnum
    {
        /// <summary>
        /// The left hand
        /// </summary>
        [EnumMember]
        Left,

        /// <summary>
        /// The right hand
        /// </summary>
        [EnumMember]
        Right
    }
}
