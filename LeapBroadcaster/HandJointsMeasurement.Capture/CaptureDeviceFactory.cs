using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandJointsMeasurement.Capture
{
    public class CaptureDeviceFactory
    {

        private static DeviceType deviceType = DeviceType.Unknown;

        public static DeviceType DeviceType
        {
            get {
                if (deviceType == DeviceType.Unknown)
                {
                    var setting = ConfigurationManager.AppSettings["DeviceType"];

                    if (setting.Equals("Realsense", StringComparison.InvariantCultureIgnoreCase))
                    {
                        deviceType = DeviceType.Realsense;
                    }
                    else
                    {
                        // Default to Leap if setting not present
                        deviceType = DeviceType.Leap;
                    }
                }

                return deviceType;
            }
        }

        public static ICaptureDevice GetCaptureDevice()
        {
            //if (DeviceType == DeviceType.Leap)
            //{
                return new LeapCaptureDevice();
            //}
            //else
            //{
            //    return new RealSenseCaptureDevice();
            //}
        }
    }
}
