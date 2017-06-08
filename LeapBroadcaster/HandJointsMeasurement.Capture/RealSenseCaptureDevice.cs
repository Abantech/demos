//using System;
//using System.Collections.Generic;
//using static PXCMHandData;

//namespace HandJointsMeasurement.Capture
//{
//    public class RealSenseCaptureDevice : ICaptureDevice
//    {
//        private PXCMSession session;
//        private PXCMSenseManager manager;
//        private PXCMHandModule handModule;
//        private PXCMHandData handDataOutput;
//        private PXCMHandConfiguration handConfiguration;
//        internal FixedSizedList<CaptureFrame> historicalFrames;

//        public string DeviceID { get; set; }

//        public DeviceStatus Initialize()
//        {
//            DeviceStatus status = null;
//            string errorMessage = null;

//            if ((session = PXCMSession.CreateInstance()) == null)
//            {
//                errorMessage = "Unable to start session.";
//                return new DeviceStatus(DeviceStatusType.Error, errorMessage);
//            }

//            if ((manager = session.CreateSenseManager()) == null)
//            {
//                errorMessage = "Unable to create sense manager.";
//                return new DeviceStatus(DeviceStatusType.Error, errorMessage);
//            }

//            if (!TrySetTracking(out errorMessage))
//            {
//                return new DeviceStatus(DeviceStatusType.Error, errorMessage);
//            }

//            if (manager.Init() != pxcmStatus.PXCM_STATUS_NO_ERROR)
//            {
//                errorMessage = "Unable to initialize sense manager.";
//                return new DeviceStatus(DeviceStatusType.Error, errorMessage);
//            }

//            if (string.IsNullOrWhiteSpace(errorMessage))
//            {
//                status = new DeviceStatus(DeviceStatusType.Running);
//            }
//            else
//            {
//            }

//            // TODO Set device ID

//            historicalFrames = new FixedSizedList<CaptureFrame>();
//            historicalFrames.Limit = 20;

//            return status;
//        }

//        public CaptureFrame GetFrame()
//        {
//            CaptureFrame frame = new CaptureFrame();

//            if (manager.AcquireFrame(false, 0) < pxcmStatus.PXCM_STATUS_NO_ERROR)
//            {
//                return frame;
//            }

//            List<IHand> handData = new List<IHand>();

//            if (handDataOutput.Update() == pxcmStatus.PXCM_STATUS_NO_ERROR)
//            {
//                for (int i = 0; i < handDataOutput.QueryNumberOfHands(); ++i)
//                {
//                    IHand hand;
//                    handDataOutput.QueryHandData(PXCMHandData.AccessOrderType.ACCESS_ORDER_BY_ID, i, out hand);
//                    handData.Add(hand);
//                }

//                frame.ID = manager.captureManager.QueryFrameIndex();
//            }

//            manager.ReleaseFrame();
//            frame.Hands = new HandData();

//            foreach (var hand in handData)
//            {
//                if (hand.QueryBodySide() == BodySideType.BODY_SIDE_LEFT)
//                {
//                    frame.Hands.LeftHand = ConvertHand(hand);
//                }
//                else
//                {
//                    frame.Hands.RightHand = ConvertHand(hand);
//                }
//            }

//            frame.TimeTaken = DateTime.Now;

//            historicalFrames.Add(frame);

//            return frame;
//        }

//        private Hand ConvertHand(IHand iHand)
//        {
//            Hand hand = new Hand();

//            hand.HandType = iHand.QueryBodySide() == BodySideType.BODY_SIDE_LEFT ? HandEnum.Left : HandEnum.Right;

//            for (int i = 0; i < 5; i++)
//            {
//                PXCMHandData.FingerType IFingerType = FingerType.FINGER_THUMB;
//                FingerEnum fingerType = FingerEnum.Thumb;

//                PXCMHandData.JointType tipType = JointType.JOINT_CENTER;
//                PXCMHandData.JointType dipType = JointType.JOINT_CENTER;
//                PXCMHandData.JointType pipType = JointType.JOINT_CENTER;
//                PXCMHandData.JointType cmcType = JointType.JOINT_CENTER;

//                switch (i)
//                {
//                    case 0:
//                        fingerType = FingerEnum.Thumb;
//                        IFingerType = FingerType.FINGER_THUMB;
//                        tipType = JointType.JOINT_THUMB_TIP;
//                        dipType = JointType.JOINT_THUMB_JT2;
//                        pipType = JointType.JOINT_THUMB_JT1;
//                        cmcType = JointType.JOINT_THUMB_BASE;
//                        break;
//                    case 1:
//                        fingerType = FingerEnum.Index;
//                        IFingerType = FingerType.FINGER_INDEX;
//                        tipType = JointType.JOINT_INDEX_TIP;
//                        dipType = JointType.JOINT_INDEX_JT2;
//                        pipType = JointType.JOINT_INDEX_JT1;
//                        cmcType = JointType.JOINT_INDEX_BASE;
//                        break;
//                    case 2:
//                        fingerType = FingerEnum.Middle;
//                        IFingerType = FingerType.FINGER_MIDDLE;
//                        tipType = JointType.JOINT_MIDDLE_TIP;
//                        dipType = JointType.JOINT_MIDDLE_JT2;
//                        pipType = JointType.JOINT_MIDDLE_JT1;
//                        cmcType = JointType.JOINT_MIDDLE_BASE;
//                        break;
//                    case 3:
//                        fingerType = FingerEnum.Ring;
//                        IFingerType = FingerType.FINGER_RING;
//                        tipType = JointType.JOINT_RING_TIP;
//                        dipType = JointType.JOINT_RING_JT2;
//                        pipType = JointType.JOINT_RING_JT1;
//                        cmcType = JointType.JOINT_RING_BASE;
//                        break;
//                    case 4:
//                        fingerType = FingerEnum.Pinky;
//                        IFingerType = FingerType.FINGER_PINKY;
//                        tipType = JointType.JOINT_PINKY_TIP;
//                        dipType = JointType.JOINT_PINKY_JT2;
//                        pipType = JointType.JOINT_PINKY_JT1;
//                        cmcType = JointType.JOINT_PINKY_BASE;
//                        break;
//                    default:
//                        break;
//                }

//                PXCMHandData.FingerData fingerData;
//                iHand.QueryFingerData(IFingerType, out fingerData);

//                JointData tip;
//                JointData dip;
//                JointData pip;
//                JointData cmc;

//                iHand.QueryTrackedJoint(tipType, out tip);
//                iHand.QueryTrackedJoint(dipType, out dip);
//                iHand.QueryTrackedJoint(pipType, out pip);
//                iHand.QueryTrackedJoint(cmcType, out cmc);

//                switch (i)
//                {
//                    case 0:
//                        hand.Thumb = ConvertFinger(fingerType, fingerData, tip, dip, pip, cmc);
//                        break;
//                    case 1:
//                        hand.Index = ConvertFinger(fingerType, fingerData, tip, dip, pip, cmc);
//                        break;
//                    case 2:
//                        hand.Middle = ConvertFinger(fingerType, fingerData, tip, dip, pip, cmc);
//                        break;
//                    case 3:
//                        hand.Ring = ConvertFinger(fingerType, fingerData, tip, dip, pip, cmc);
//                        break;
//                    case 4:
//                        hand.Pinky = ConvertFinger(fingerType, fingerData, tip, dip, pip, cmc);
//                        break;
//                    default:
//                        break;
//                }
//            }

//            JointData palm;
//            iHand.QueryTrackedJoint(JointType.JOINT_CENTER, out palm);

//            JointData wrist;
//            iHand.QueryTrackedJoint(JointType.JOINT_WRIST, out wrist);

//            hand.Palm = JointDataToVector3(palm);
//            hand.Wrist = JointDataToVector3(wrist);

//            return hand;
//        }

//        private Finger ConvertFinger(FingerEnum fingerType, FingerData fingerData, JointData tip, JointData dip, JointData pip, JointData cmc)
//        {
//            var finger = new Finger()
//            {
//                FingerType = fingerType,
//                TIPPosition = JointDataToVector3(tip),
//                DIPPosition = JointDataToVector3(dip),
//                PIPPosition = JointDataToVector3(pip)
//            };

//            finger.CMCPosition = JointDataToVector3(cmc);
//            return finger;
//        }

//        private Vector3 JointDataToVector3(JointData data)
//        {
//            return new Vector3(data.positionWorld.x, data.positionWorld.y, data.positionWorld.z);
//        }

//        private bool TrySetTracking(out string errorMessage)
//        {
//            if (manager.EnableHand() != pxcmStatus.PXCM_STATUS_NO_ERROR)
//            {
//                errorMessage = "Cannot enable hand.";
//                return false;
//            }

//            handModule = manager.QueryHand();
//            if (handModule == null)
//            {
//                errorMessage = "Cannot enable hand module.";
//                return false;
//            }

//            handDataOutput = handModule.CreateOutput();
//            if (handDataOutput == null)
//            {
//                errorMessage = "Cannot create hand output.";
//                return false;
//            }

//            handConfiguration = handModule.CreateActiveConfiguration();
//            if (handConfiguration == null)
//            {
//                errorMessage = "Cannot create hand configuration.";
//                return false;
//            }

//            handConfiguration.SetTrackingMode(PXCMHandData.TrackingModeType.TRACKING_MODE_FULL_HAND);
//            errorMessage = null;
//            return true;
//        }

//        public void Dispose()
//        {
//            manager.Close();
//            manager.Dispose();
//            session.Dispose();
//        }

//        public IEnumerable<CaptureFrame> GetHistoricalFrames(int count)
//        {
//            IEnumerable<CaptureFrame> frames = new List<CaptureFrame>();

//            if (historicalFrames != null)
//            {
//                if (historicalFrames.Count() < count)
//                {
//                    count = historicalFrames.Count();
//                }

//                frames = historicalFrames.Get(count);
//            }

//            return frames;
//        }
//    }
//}
