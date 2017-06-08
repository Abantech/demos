using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using Leap;

namespace HandJointsMeasurement.Capture
{
    /// <summary>
    /// The Leap Motion capture device
    /// </summary>
    /// <seealso cref="HandJointsMeasurement.ICaptureDevice" />
    public class LeapCaptureDevice : ICaptureDevice
    {
        private Leap.Controller leapController;
        private DeviceStatus deviceStatus;
        private FixedSizedList<CaptureFrame> historicalFrames;

        public string DeviceID { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LeapCaptureDevice"/> class.
        /// </summary>
        public LeapCaptureDevice()
        {
            leapController = new Leap.Controller();
            leapController.DeviceFailure += LeapController_DeviceFailure;
            leapController.DeviceLost += OnLeapControllerNoLongerRunning;
            leapController.Disconnect += OnLeapControllerNoLongerRunning;
            historicalFrames = new FixedSizedList<CaptureFrame>();
            historicalFrames.Limit = 20;
        }
        public CaptureFrame GetFrame()
        {
            CaptureFrame frame = new CaptureFrame();

            frame.Hands = new HandData();

            List<Leap.Hand> handData = leapController.Frame().Hands;

            foreach (var hand in handData)
            {
                if (hand.IsLeft)
                {
                    frame.Hands.LeftHand = ConvertHand(hand);
                }
                else
                {
                    frame.Hands.RightHand = ConvertHand(hand);
                }
            }

            frame.TimeTaken = DateTime.Now;

            historicalFrames.Add(frame);
            return frame;
        }

        private Hand ConvertHand(Leap.Hand leapHand)
        {
            return new Hand()
            {
                HandType = leapHand.IsLeft ? HandEnum.Left : HandEnum.Right,
                Thumb = ConvertFinger(leapHand.Fingers[0]),
                Index = ConvertFinger(leapHand.Fingers[1]),
                Middle = ConvertFinger(leapHand.Fingers[2]),
                Ring = ConvertFinger(leapHand.Fingers[3]),
                Pinky = ConvertFinger(leapHand.Fingers[4]),
                Palm = Vector3FromLeapVector(leapHand.PalmPosition),
                Wrist = Vector3FromLeapVector(leapHand.WristPosition)
            };
        }

        private Finger ConvertFinger(Leap.Finger leapFinger)
        {
            return new Finger()
            {
                FingerType = LeapFingerTypeToAbantechFingerType(leapFinger.Type),
                TIPPosition = Vector3FromLeapVector(leapFinger.TipPosition),
                DIPPosition = Vector3FromLeapVector(leapFinger.Bone(Bone.BoneType.TYPE_DISTAL).PrevJoint),
                PIPPosition = Vector3FromLeapVector(leapFinger.Bone(Bone.BoneType.TYPE_INTERMEDIATE).PrevJoint),
                CMCPosition = Vector3FromLeapVector(leapFinger.Bone(Bone.BoneType.TYPE_PROXIMAL).PrevJoint)
            };
        }

        private Vector3 Vector3FromLeapVector(Vector v)
        {
            return new Vector3(v.x / 1000, v.y / 1000, v.z / 1000);
        }

        private FingerEnum LeapFingerTypeToAbantechFingerType(Leap.Finger.FingerType fingerType)
        {
            switch (fingerType)
            {
                case Leap.Finger.FingerType.TYPE_THUMB:
                    return FingerEnum.Thumb;
                case Leap.Finger.FingerType.TYPE_INDEX:
                    return FingerEnum.Index;
                case Leap.Finger.FingerType.TYPE_MIDDLE:
                    return FingerEnum.Middle;
                case Leap.Finger.FingerType.TYPE_RING:
                    return FingerEnum.Ring;
                case Leap.Finger.FingerType.TYPE_PINKY:
                    return FingerEnum.Pinky;
                case Leap.Finger.FingerType.TYPE_UNKNOWN:
                    return FingerEnum.Unknown;
                default:
                    return FingerEnum.Unknown;
            }
        }

        public DeviceStatus Initialize()
        {
            //TODO: Check machine configuration for how many frames to keep before throwing away

            if (!leapController.IsConnected)
            {
                if (!leapController.IsServiceConnected)
                {
                    ServiceController leapService;
                    //Restart leap service (happens often when system comes out of sleep that leap service is not started)
                    try
                    {
                        leapService = new ServiceController("LeapService");
                        this.deviceStatus = new DeviceStatus(DeviceStatusType.Initializing);
                        leapService.WaitForStatus(ServiceControllerStatus.Running, new TimeSpan(0, 0, 10));
                    }
                    catch (Exception)
                    {
                        return new DeviceStatus(DeviceStatusType.Error, "Unable to start Leap service. Either the service is not installed or the device is not connected.");
                    }
                }

                leapController.StartConnection();
            }

            return this.deviceStatus;
        }

        private void OnLeapControllerNoLongerRunning(object sender, EventArgs e)
        {
            this.deviceStatus = new DeviceStatus(DeviceStatusType.Unresponsive);
        }

        private void LeapController_DeviceFailure(object sender, Leap.DeviceFailureEventArgs e)
        {
            this.deviceStatus = new DeviceStatus(DeviceStatusType.Error, String.Format("Device Failure by Leap '{0}', ErrorCode : {1},  Message: {2} (", e.type, e.ErrorCode, e.ErrorMessage));
        }

        public void Dispose()
        {
            leapController.Dispose();
        }

        public IEnumerable<CaptureFrame> GetHistoricalFrames(int count)
        {
            IEnumerable<CaptureFrame> frames = new List<CaptureFrame>();

            if (historicalFrames != null)
            {
                if (historicalFrames.Count() < count)
                {
                    count = historicalFrames.Count();
                }

                frames = historicalFrames.Get(count);
            }

            return frames;
        }
    }
}
