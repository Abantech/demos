namespace HandJointsMeasurement
{
    public interface IFrameProcessorAction
    {
        IHandDataFrame ProcessFrame(IHandDataFrame frame);
    }
}