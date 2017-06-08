using System.Runtime.Serialization;

namespace HandJointsMeasurement
{
    [DataContract]
    public class Hand
    {
        /// <summary>
        /// Gets the type of the hand.
        /// </summary>
        /// <value>
        /// The type of the hand.
        /// </value>
        [DataMember]
        public HandEnum HandType { get; set; }

        /// <summary>
        /// Gets the thumb.
        /// </summary>
        /// <value>
        /// The thumb.
        /// </value>
        [DataMember]
        public Finger Thumb { get; set; }

        /// <summary>
        /// Gets the index finger.
        /// </summary>
        /// <value>
        /// The index finger.
        /// </value>
        [DataMember]
        public Finger Index { get; set; }

        /// <summary>
        /// Gets the middle finger.
        /// </summary>
        /// <value>
        /// The middle finger.
        /// </value>
        [DataMember]
        public Finger Middle { get; set; }

        /// <summary>
        /// Gets the ring finger.
        /// </summary>
        /// <value>
        /// The ring finger.
        /// </value>
        [DataMember]
        public Finger Ring { get; set; }

        /// <summary>
        /// Gets the pinky finger.
        /// </summary>
        /// <value>
        /// The pinky finger.
        /// </value>
        [DataMember]
        public Finger Pinky { get; set; }

        [DataMember]
        public Vector3 Palm { get; set; }

        [DataMember]
        public Vector3 Wrist { get; set; }
    }
}
