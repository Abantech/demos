using System;
using System.Linq;
using System.Collections.Generic;
using System.Security;
using System.Timers;
using HandJointsMeasurement.Capture;
using HandJointsMeasurement.Processing;
using System.Text;

namespace HandJointsMeasurement
{
    public class Session
    {
        //private IDatabaseConnector dbConnector;
        private int fps = 1000 / 50;
        ICaptureDevice device;
        Timer poller;
        private List<IHandDataFrame> frames;
        private DateTime lastTimePolledForFrame = DateTime.Now;
        //private IDatabaseConnector dbConnector;
        private SessionSummary summary;

        public DateTime SessionStart { get; private set; }

        public string OperatorID { get; set; }

        public DateTime SessionEnd { get; private set; }

        public int SessionLifetime { get; set; } = 60;

        public string SessionID
        {
            get; private set;
        }

        public bool IsRunning { get; private set; }

        public Session(string operatorID)
        {
            this.SessionID = Guid.NewGuid().ToString();
            this.device = CaptureDeviceFactory.GetCaptureDevice();
            this.frames = new List<IHandDataFrame>();
            this.OperatorID = operatorID;
            this.summary = new SessionSummary();
            this.ValidateOperator();
        }
        public T GetLatestFrame<T>() where T : IHandDataFrame
        {
            lastTimePolledForFrame = DateTime.Now;

            if (this.frames.Count > 0)
            {
                var typedFrames = this.frames.Where(x => x.GetType() == typeof(T));
                if (typedFrames.Count() > 0)
                {
                    T latestFrame = (T)typedFrames.Last();
                    Frame frame = new Frame(latestFrame.Hands);
                    //dbConnector.WriteData(frame);
                    return latestFrame;
                }
            }
            
            return default(T);
        }

        public void Start()
        {
            device.Initialize();

            poller = new Timer(fps);
            poller.Elapsed += new ElapsedEventHandler(PollInput);
            poller.Enabled = true;

            this.SessionStart = DateTime.Now;
            this.summary.StartTime = this.SessionStart;
            this.IsRunning = true;

            //dbConnector = DatabaseConnectionFactory.GetDatabaseConnector();
            //dbConnector.CreateSessionRecord(this.SessionID, this.OperatorID, DateTime.Now);
        }

        public SessionSummary End()
        {
            poller.Enabled = false;
            device.Dispose();

            this.SessionEnd = DateTime.Now; // Check to make sure is after SessionStart? What if not?
            this.summary.EndTime = this.SessionEnd;
            //dbConnector.EndSession(this.summary);

            return this.summary;
            
            //TODO: Close record and write to local persistence store
            //TODO: prompt user for Subject? How? Present options from Google Calendar?
            //TODO: Actions? Encrypt with cert?
        }

        void PollInput(object sender, ElapsedEventArgs e)
        {
            if (DateTime.Now > lastTimePolledForFrame.AddSeconds(this.SessionLifetime))
            {
                IsRunning = false;
                this.End();
                return;
            }
            
            this.frames.Add(FrameProcessor.GetInstance().ProcessFrames(device.GetFrame()));
        }

        private void ValidateOperator()
        {
            // TODO: Do check on Operator ID
            if (false)
            {
                throw new SecurityException();
            }
        }
    }
}
