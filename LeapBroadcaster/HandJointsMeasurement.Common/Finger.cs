using System;
using System.Runtime.Serialization;

namespace HandJointsMeasurement
{
    [DataContract]
    public class Finger
    {
        [DataMember]
        public FingerEnum FingerType { get; set; }

        [DataMember]
        public Vector3 TIPPosition { get; set; }
        [DataMember]
        public Vector3 DIPPosition { get; set; }
        [DataMember]
        public Vector3 PIPPosition { get; set; }
        [DataMember]
        public Vector3 CMCPosition { get; set; }
        [DataMember]
        public Vector3 RCPosition { get; set; }

        public float MCPAngle(Vector3 palmPosition)
        {
            var mcpPipVector = PIPPosition - CMCPosition;
            var palmMCPVector = CMCPosition - palmPosition;
            return mcpPipVector.AngleTo(palmMCPVector);
        }

        public float DIPAngle()
        {
            var dipTipVector = TIPPosition - DIPPosition;
            var pipDipVector = DIPPosition - PIPPosition;
            return dipTipVector.AngleTo(pipDipVector);
        }

        public float PIPAngle()
        {
            var pipDipVector = DIPPosition - PIPPosition;
            var cmcPipVector = PIPPosition - CMCPosition;
            return pipDipVector.AngleTo(cmcPipVector);
        }
    }
}
