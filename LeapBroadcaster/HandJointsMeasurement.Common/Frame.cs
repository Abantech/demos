using System.Drawing;
using System.Runtime.Serialization;

namespace HandJointsMeasurement
{
    /// <summary>
    /// Contains data and images captured by the device
    /// </summary>
    [DataContract]
    public class Frame
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Frame"/> class.
        /// </summary>
        /// <param name="handData">The hand data.</param>
        /// <param name="imageLeft">The left from the device.</param>
        /// <param name="imageRight">The right image from the device.</param>
        public Frame(HandData handData, Bitmap imageLeft, Bitmap imageRight)
        {
            this.HandData = handData;
            this.LeftImage = imageLeft;
            this.RightImage = imageRight;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Frame"/> class.
        /// </summary>
        /// <param name="handData">The hand data.</param>
        public Frame(HandData handData)
        {
            this.HandData = handData;
        }

        /// <summary>
        /// Gets the hand data.
        /// </summary>
        /// <value>
        /// The hand data.
        /// </value>
        [DataMember]
        public HandData HandData { get; set; }

        /// <summary>
        /// Gets the left image.
        /// </summary>
        /// <value>
        /// The left image from the device.
        /// </value>
        [DataMember]
        public Bitmap LeftImage { get; set; }

        /// <summary>
        /// Gets the right image.
        /// </summary>
        /// <value>
        /// The right image from the device.
        /// </value>
        [DataMember]
        public Bitmap RightImage { get; set; }
    }
}
