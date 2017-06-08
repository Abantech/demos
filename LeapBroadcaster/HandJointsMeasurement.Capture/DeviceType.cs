using System.Runtime.Serialization;

namespace HandJointsMeasurement.Capture
{
    [DataContract]
    public enum DeviceType
    {
        [EnumMember]
        Leap,

        [EnumMember]
        Realsense,

        [EnumMember]
        Unknown
    }
}