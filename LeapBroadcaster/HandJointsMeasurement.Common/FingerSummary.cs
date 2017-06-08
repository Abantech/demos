using System;
using System.Runtime.Serialization;

namespace HandJointsMeasurement
{
    [DataContract]
    public class FingerSummary
    {
        [DataMember]
        public JointSummary DIP { get; set; }

        [DataMember]
        public JointSummary PIP { get; set; }

        [DataMember]
        public JointSummary MCP { get; set; }

        internal void AddData(Finger finger, Vector3 palmPosition)
        {
            DIP.AddData(finger.DIPAngle());
            PIP.AddData(finger.PIPAngle());
            MCP.AddData(finger.MCPAngle(palmPosition));
        }
    }
}