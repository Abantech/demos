using HandJointsMeasurement.Capture;
using System.Runtime.Serialization;

namespace HandJointsMeasurement.Service
{
    [DataContract]
    public class SessionData
    {
        [DataMember]
        public string SessionID { get; set; }


        [DataMember]
        public DeviceType DeviceType { get; set; }
    }
}