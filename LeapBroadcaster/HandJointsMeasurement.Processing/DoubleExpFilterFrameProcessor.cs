using System;
using System.Linq;

namespace HandJointsMeasurement.Processing
{
    public class DoubleExpFilterFrameProcessor : IFrameProcessorAction
    {
        public int JointCount { get; set; } = 50;

        public struct TRANSFORM_SMOOTH_PARAMETERS
        {
            public float fSmoothing;             // [0..1], lower values closer to raw data
            public float fCorrection;            // [0..1], lower values slower to correct towards the raw data
            public float fPrediction;            // [0..n], the number of frames to predict into the future
            public float fJitterRadius;          // The radius in meters for jitter reduction
            public float fMaxDeviationRadius;    // The maximum radius in meters that filtered positions are allowed to deviate from raw data
        }

        public class FilterDoubleExponentialData
        {
            public Vector3 m_vRawPosition;
            public Vector3 m_vFilteredPosition;
            public Vector3 m_vTrend;
            public int m_dwFrameCount;
        }

        // Holt Double Exponential Smoothing filter
        Vector3[] m_pFilteredJoints;
        FilterDoubleExponentialData[] m_pHistory;
        float m_fSmoothing;
        float m_fCorrection;
        float m_fPrediction;
        float m_fJitterRadius;
        float m_fMaxDeviationRadius;

        public DoubleExpFilterFrameProcessor()
        {
            m_pFilteredJoints = new Vector3[JointCount];
            m_pHistory = new FilterDoubleExponentialData[JointCount];
            for (int i = 0; i < JointCount; i++)
            {
                m_pHistory[i] = new FilterDoubleExponentialData();
            }

            Init();
        }

        public void Init(float fSmoothing = 0.25f, float fCorrection = 0.25f, float fPrediction = 0.25f, float fJitterRadius = 0.03f, float fMaxDeviationRadius = 0.05f)
        {
            Reset(fSmoothing, fCorrection, fPrediction, fJitterRadius, fMaxDeviationRadius);
        }

        public void Reset(float fSmoothing = 0.25f, float fCorrection = 0.25f, float fPrediction = 0.25f, float fJitterRadius = 0.03f, float fMaxDeviationRadius = 0.05f)
        {
            if (m_pFilteredJoints == null || m_pHistory == null)
            {
                return;
            }

            m_fMaxDeviationRadius = fMaxDeviationRadius; // Size of the max prediction radius Can snap back to noisy data when too high
            m_fSmoothing = fSmoothing;                   // How much smothing will occur.  Will lag when too high
            m_fCorrection = fCorrection;                 // How much to correct back from prediction.  Can make things springy
            m_fPrediction = fPrediction;                 // Amount of prediction into the future to use. Can over shoot when too high
            m_fJitterRadius = fJitterRadius;             // Size of the radius where jitter is removed. Can do too much smoothing when too high

            for (int i = 0; i < JointCount; i++)
            {
                m_pFilteredJoints[i] = new Vector3(0, 0, 0);
                m_pHistory[i].m_vFilteredPosition = new Vector3(0, 0, 0);
                m_pHistory[i].m_vRawPosition = new Vector3(0, 0, 0);
                m_pHistory[i].m_vTrend = new Vector3(0, 0, 0);

                m_pHistory[i].m_dwFrameCount = 0;
            }
        }

        //--------------------------------------------------------------------------------------
        // Implementation of a Holt Double Exponential Smoothing filter. The double exponential
        // smooths the curve and predicts.  There is also noise jitter removal. And maximum
        // prediction bounds.  The paramaters are commented in the init function.
        //--------------------------------------------------------------------------------------
        private ProcessedFrame GetFilteredJoints(IHandDataFrame frame)
        {
            if (frame.Hands == null || (frame.Hands.RightHand == null && frame.Hands.LeftHand == null))
            {
                return new ProcessedFrame(frame.Hands);
            }

            HandData data = new HandData();
            if (frame.Hands.LeftHand != null)
            {
                data.LeftHand = this.FilterHandData(HandEnum.Left, frame.Hands.LeftHand);
            }

            if (frame.Hands.RightHand != null)
            {
                data.RightHand = this.FilterHandData(HandEnum.Right, frame.Hands.RightHand);
            }

            return new ProcessedFrame(data);
        }

        private Hand FilterHandData(HandEnum side, Hand hand)
        {
            return new Hand()
            {
                Thumb = this.FilterFingerData(hand.Thumb, hand.HandType),
                Index = this.FilterFingerData(hand.Index, hand.HandType),
                Middle = this.FilterFingerData(hand.Middle, hand.HandType),
                Ring = this.FilterFingerData(hand.Ring, hand.HandType),
                Pinky = this.FilterFingerData(hand.Pinky, hand.HandType),
                HandType = hand.HandType,
                Palm = hand.Palm,
                Wrist = hand.Wrist
            };
        }

        private Finger FilterFingerData(Finger finger, HandEnum handType)
        {
            return new Finger()
            {
                FingerType = finger.FingerType,
                TIPPosition = this.FilterJointData(finger.TIPPosition, handType, finger.FingerType, 0),
                DIPPosition = this.FilterJointData(finger.DIPPosition, handType, finger.FingerType, 1),
                PIPPosition = this.FilterJointData(finger.PIPPosition, handType, finger.FingerType, 2),
                CMCPosition = this.FilterJointData(finger.CMCPosition, handType, finger.FingerType, 3),
                RCPosition = this.FilterJointData(new Vector3(0,0,0), handType, finger.FingerType, 4),
            };
        }

        //--------------------------------------------------------------------------------------
        // if joint is 0 it is not valid.
        //--------------------------------------------------------------------------------------
        bool JointPositionIsValid(Vector3 vJointPosition)
        {
            return (vJointPosition.X != 0.0f ||
                        vJointPosition.Y != 0.0f ||
                        vJointPosition.Z != 0.0f);
        }

        Vector3 CSVectorZero()
        {
            Vector3 point = new Vector3();

            point.X = 0.0f;
            point.Y = 0.0f;
            point.Z = 0.0f;

            return point;
        }

        Vector3 CSVectorAdd(Vector3 p1, Vector3 p2)
        {
            Vector3 sum = new Vector3();

            sum.X = p1.X + p2.X;
            sum.Y = p1.Y + p2.Y;
            sum.Z = p1.Z + p2.Z;

            return sum;
        }

        Vector3 CSVectorScale(Vector3 p, float scale)
        {
            Vector3 point = new Vector3();

            point.X = p.X * scale;
            point.Y = p.Y * scale;
            point.Z = p.Z * scale;

            return point;
        }

        Vector3 CSVectorSubtract(Vector3 p1, Vector3 p2)
        {
            Vector3 diff = new Vector3();

            diff.X = p1.X - p2.X;
            diff.Y = p1.Y - p2.Y;
            diff.Z = p1.Z - p2.Z;

            return diff;
        }

        float CSVectorLength(Vector3 p)
        {
            return (float)Math.Sqrt(p.X * p.X + p.Y * p.Y + p.Z * p.Z);
        }

        Vector3 FilterJointData(Vector3 joint, HandEnum handType, FingerEnum fingerType, int jointNumber)
        {
            Vector3 vPrevRawPosition;
            Vector3 vPrevFilteredPosition;
            Vector3 vPrevTrend;
            Vector3 vRawPosition;
            Vector3 vFilteredPosition;
            Vector3 vPredictedPosition;
            Vector3 vDiff;
            Vector3 vTrend;
            float fDiff;
            bool bJointIsValid;

            int index = GetIndexForJoint(handType, fingerType, jointNumber);

            vRawPosition = joint;
            vPrevFilteredPosition = m_pHistory[index].m_vFilteredPosition;
            vPrevTrend = m_pHistory[index].m_vTrend;
            vPrevRawPosition = m_pHistory[index].m_vRawPosition;
            bJointIsValid = JointPositionIsValid(vRawPosition);

            // If joint is invalid, reset the filter
            if (!bJointIsValid)
            {
                m_pHistory[index].m_dwFrameCount = 0;
            }

            // Initial start values
            if (m_pHistory[index].m_dwFrameCount == 0)
            {
                vFilteredPosition = vRawPosition;
                vTrend = CSVectorZero();
                m_pHistory[index].m_dwFrameCount++;
            }
            else if (m_pHistory[index].m_dwFrameCount == 1)
            {
                vFilteredPosition = CSVectorScale(CSVectorAdd(vRawPosition, vPrevRawPosition), 0.5f);
                vDiff = CSVectorSubtract(vFilteredPosition, vPrevFilteredPosition);
                vTrend = CSVectorAdd(CSVectorScale(vDiff, m_fCorrection), CSVectorScale(vPrevTrend, 1.0f - m_fCorrection));
                m_pHistory[index].m_dwFrameCount++;
            }
            else
            {
                // First apply jitter filter
                vDiff = CSVectorSubtract(vRawPosition, vPrevFilteredPosition);
                fDiff = CSVectorLength(vDiff);

                if (fDiff <= m_fJitterRadius)
                {
                    vFilteredPosition = CSVectorAdd(CSVectorScale(vRawPosition, fDiff / m_fJitterRadius),
                        CSVectorScale(vPrevFilteredPosition, 1.0f - fDiff / m_fJitterRadius));
                }
                else
                {
                    vFilteredPosition = vRawPosition;
                }

                // Now the double exponential smoothing filter
                vFilteredPosition = CSVectorAdd(CSVectorScale(vFilteredPosition, 1.0f - m_fSmoothing),
                    CSVectorScale(CSVectorAdd(vPrevFilteredPosition, vPrevTrend), m_fSmoothing));


                vDiff = CSVectorSubtract(vFilteredPosition, vPrevFilteredPosition);
                vTrend = CSVectorAdd(CSVectorScale(vDiff, m_fCorrection), CSVectorScale(vPrevTrend, 1.0f - m_fCorrection));
            }

            // Predict into the future to reduce latency
            vPredictedPosition = CSVectorAdd(vFilteredPosition, CSVectorScale(vTrend, m_fPrediction));

            // Check that we are not too far away from raw data
            vDiff = CSVectorSubtract(vPredictedPosition, vRawPosition);
            fDiff = CSVectorLength(vDiff);

            if (fDiff > m_fMaxDeviationRadius)
            {
                vPredictedPosition = CSVectorAdd(CSVectorScale(vPredictedPosition, m_fMaxDeviationRadius / fDiff),
                    CSVectorScale(vRawPosition, 1.0f - m_fMaxDeviationRadius / fDiff));
            }

            // Save the data from this frame
            m_pHistory[index].m_vRawPosition = vRawPosition;
            m_pHistory[index].m_vFilteredPosition = vFilteredPosition;
            m_pHistory[index].m_vTrend = vTrend;

            // Output the data
            m_pFilteredJoints[index] = vPredictedPosition;

            return vPredictedPosition;
        }

        private int GetIndexForJoint(HandEnum handType, FingerEnum fingerType, int jointNumber)
        {
            int index = 0;

            if (handType == HandEnum.Right)
            {
                index += 25;
            }

            switch (fingerType)
            {
                case FingerEnum.Thumb:
                    break;
                case FingerEnum.Index:
                    index += 5;
                    break;
                case FingerEnum.Middle:
                    index += 10;
                    break;
                case FingerEnum.Ring:
                    index += 15;
                    break;
                case FingerEnum.Pinky:
                    index += 20;
                    break;
                case FingerEnum.Unknown:
                    break;
                default:
                    break;
            }

            return index + jointNumber;
        }

        public IHandDataFrame ProcessFrame(IHandDataFrame rawFrame)
        {
            return this.GetFilteredJoints(rawFrame);
        }
    }
}
