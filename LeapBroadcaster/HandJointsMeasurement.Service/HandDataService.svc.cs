using HandJointsMeasurement.Capture;
using HandJointsMeasurement.Processing;
using System.Configuration;

namespace HandJointsMeasurement.Service
{
    public class HandDataService : IHandDataService
    {

        static HandDataService()
        {
            if (ConfigurationManager.AppSettings["Filters"] == "DoubleExponential")
            {
                FrameProcessor.AddFrameProcessorAction(new DoubleExpFilterFrameProcessor());
            }
        }

        public SessionData StartSession(string operatorID)
        {
            var sessionData = new SessionData();
            sessionData.SessionID= SessionManager.CreateSession(operatorID);
            SessionManager.StartSession(sessionData.SessionID);
            sessionData.DeviceType = CaptureDeviceFactory.DeviceType;

            return sessionData;
        }
        public SessionSummary EndSession(string sessionID)
        {
            SessionSummary summary = null;
            if (SessionManager.HasSession(sessionID))
            {
                summary = SessionManager.EndSession(sessionID);
            }
            else
            {
                // TODO
            }

            return summary;
        }

        public ProcessedFrame GetFrame(string sessionID, bool includeImages, bool includeDepth)
        {
            ProcessedFrame frame = null;
            if (SessionManager.HasSession(sessionID))
            {
                frame = SessionManager.GetLatestFrame(sessionID);
            }

            return frame;
        }
    }
}