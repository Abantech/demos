using System;
using System.Runtime.Serialization;

namespace HandJointsMeasurement
{
    [DataContract]
    public class HandSummary
    {
        [DataMember]
        public FingerSummary Thumb { get; set; }

        [DataMember]
        public FingerSummary Index { get; set; }

        [DataMember]
        public FingerSummary Middle { get; set; }

        [DataMember]
        public FingerSummary Ring { get; set; }

        [DataMember]
        public FingerSummary Pinky { get; set; }

        internal void AddData(Hand hand)
        {
            Thumb.AddData(hand.Thumb, hand.Palm);
            Index.AddData(hand.Index, hand.Palm);
            Middle.AddData(hand.Middle, hand.Palm);
            Ring.AddData(hand.Ring, hand.Palm);
            Pinky.AddData(hand.Pinky, hand.Palm);
        }
    }
}