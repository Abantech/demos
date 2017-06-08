using System;
using System.Runtime.Serialization;

namespace HandJointsMeasurement
{
    [DataContract]
    public class SessionSummary
    {
        [DataMember]
        public DateTime StartTime { get; set; }

        [DataMember]
        public DateTime EndTime { get; set; }

        [DataMember]
        public HandSummary LeftHand { get; set; }

        [DataMember]
        public HandSummary RightHand { get; set; }

        public void AddFrame(Frame frame)
        {
            var handData = frame.HandData;
            if (handData != null)
            {
                if (handData.LeftHand != null)
                {
                    LeftHand.AddData(handData.LeftHand);
                }
                if (handData.RightHand != null)
                {
                    RightHand.AddData(handData.RightHand);
                }
            }
        }
    }
}