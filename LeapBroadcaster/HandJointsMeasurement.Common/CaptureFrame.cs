using System;

namespace HandJointsMeasurement
{
    public class CaptureFrame : IHandDataFrame
    {
        public bool IsCorrected
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Note: set this property from the capture device to avoid issues with clock sync
        /// </summary>
        public DateTime TimeTaken { get; set; }

        public HandData Hands { get; set; }

        public object ImageData { get; set; }
        
        public object DepthData { get; set; }

        public int ID { get; set; }
    }
}