using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandJointsMeasurement.Processing
{
    class PassthroughFrameProcessorAction : IFrameProcessorAction
    {
        public IHandDataFrame ProcessFrame(IHandDataFrame frame)
        {
            return new ProcessedFrame(frame.Hands);
        }
    }
}
