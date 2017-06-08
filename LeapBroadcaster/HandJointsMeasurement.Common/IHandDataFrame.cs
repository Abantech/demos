using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandJointsMeasurement
{
    public interface IHandDataFrame
    {
        bool IsCorrected { get; }

        HandData Hands { get; }
    }
}
