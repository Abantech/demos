using System;
using System.Collections.Generic;
using System.Linq;

namespace HandJointsMeasurement.Processing
{
    public class FrameProcessor
    {
        private static FrameProcessor instance;

        public static FrameProcessor GetInstance()
        {
            if (instance == null)
            {
                instance = new FrameProcessor();
            }

            return instance;
        }

        public static void AddFrameProcessorAction(IFrameProcessorAction action)
        {
            GetInstance().actions.Add(action);
        }

        private List<IFrameProcessorAction> actions;

        private FrameProcessor()
        {
            actions = new List<IFrameProcessorAction>();
            actions.Add(new PassthroughFrameProcessorAction());
        }

        public IHandDataFrame ProcessFrames(IHandDataFrame frame)
        {
            foreach (var action in actions)
            {
                frame = action.ProcessFrame(frame);
            }

            return frame;
        }
    }
}
