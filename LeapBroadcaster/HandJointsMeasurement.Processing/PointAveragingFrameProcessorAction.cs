using System.Collections.Generic;
using System.Linq;

namespace HandJointsMeasurement.Processing
{
    class PointAveragingFrameProcessorAction : IFrameProcessorAction
    {

        public IHandDataFrame ProcessFrame(IHandDataFrame[] rawFrames)
        {
            var handData = new HandData();

            handData.LeftHand = AverageHandData(rawFrames.Select(x => x.Hands.LeftHand));
            handData.RightHand = AverageHandData(rawFrames.Select(x => x.Hands.RightHand));

            var processedFrame = new ProcessedFrame(handData);

            return processedFrame;
        }

        private Hand AverageHandData(IEnumerable<Hand> hands)
        {
            return new Hand()
            {
                Thumb = AverageFingerData(hands.Select(x => x.Thumb)),
                Index = AverageFingerData(hands.Select(x => x.Index)),
                Middle = AverageFingerData(hands.Select(x => x.Middle)),
                Ring = AverageFingerData(hands.Select(x => x.Ring)),
                Pinky = AverageFingerData(hands.Select(x => x.Pinky)),
                HandType = hands.First().HandType
            };
        }

        private Finger AverageFingerData(IEnumerable<Finger> fingers)
        {
            return new Finger()
            {
                CMCPosition = AveragePositions(fingers.Select(x => x.CMCPosition)),
                DIPPosition = AveragePositions(fingers.Select(x => x.DIPPosition)),
                PIPPosition = AveragePositions(fingers.Select(x => x.PIPPosition)),
                RCPosition = AveragePositions(fingers.Select(x => x.RCPosition)),
                TIPPosition = AveragePositions(fingers.Select(x => x.TIPPosition)),
                FingerType = fingers.First().FingerType,
            };
        }

        private Vector3 AveragePositions(IEnumerable<Vector3> positions)
        {
            return new Vector3(
                positions.Average(v => v.X),
                positions.Average(v => v.Y),
                positions.Average(v => v.Z));
        }
    }
}