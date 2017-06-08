using System;

namespace HandJointsMeasurement
{
    public class DeviceStatus
    {
        public DeviceStatus(DeviceStatusType statusType, string additionalInfo)
        {
            this.StatusType = statusType;
            this.AdditionalInfo = additionalInfo;
            this.ReportedTime = DateTime.Now;
        }

        public DeviceStatus(DeviceStatusType statusType) : this(statusType, string.Empty)
        {
        }

        public DeviceStatusType StatusType { get; private set; }

        public DateTime ReportedTime { get; private set; }

        public string AdditionalInfo { get; private set; }
    }
}
