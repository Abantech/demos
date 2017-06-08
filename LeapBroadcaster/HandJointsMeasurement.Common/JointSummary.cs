using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace HandJointsMeasurement
{
    [DataContract]
    public class JointSummary
    {
        private List<float> maximumFlexion = new List<float>();

        [DataMember]
        public float[] MaximumFlexion
        {
            get
            {
                return maximumFlexion.ToArray();
            }
            set
            {
                maximumFlexion = new List<float>(value);
            }
        }


        private List<float> maximumExtension = new List<float>();

        [DataMember]
        public float[] MaximumExtension
        {
            get
            {
                return maximumExtension.ToArray();
            }
            set
            {
                maximumExtension = new List<float>(value);
            }
        }

        internal void AddData(float v)
        {
            if (maximumFlexion.Count < 50)
            {
                maximumFlexion.Add(v);
                maximumFlexion.Sort();
            }
            else
            {
                if (maximumFlexion[0] < v)
                {
                    maximumFlexion[0] = v;
                    maximumFlexion.Sort();
                }
            }

            if (maximumExtension.Count < 50)
            {
                maximumExtension.Add(v);
                maximumExtension.Sort((a, b) => -1 * a.CompareTo(b));
            }
            else
            {
                if (maximumFlexion[0] > v)
                {
                    maximumFlexion[0] = v;
                    maximumFlexion.Sort((a, b) => -1 * a.CompareTo(b));
                }
            }
        }
    }
}
