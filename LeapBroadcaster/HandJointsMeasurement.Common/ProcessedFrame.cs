using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HandJointsMeasurement
{
    [DataContract]
    public class ProcessedFrame : IHandDataFrame
    {
        private HandData handData;

        public ProcessedFrame(HandData handData)
        {
            UpdateHandData(handData);
        }

        [DataMember]
        public HandData Hands
        {
            get
            {
                return handData;
            }
            set
            {

            }
        }

        public void UpdateHandData(HandData handData)
        {
            this.handData = handData;
        }

        [DataMember]
        public bool IsCorrected
        {
            get
            {
                return true;
            }
            set
            {

            }
        }
    }
}