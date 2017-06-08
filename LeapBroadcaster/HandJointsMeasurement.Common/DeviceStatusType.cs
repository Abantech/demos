namespace HandJointsMeasurement
{
    /// <summary>
    /// Enumeration of the device statuses
    /// </summary>
    public enum DeviceStatusType
    {
        /// <summary>
        /// The device is running
        /// </summary>
        Running,

        /// <summary>
        /// The device is intializing
        /// </summary>
        Initializing,

        /// <summary>
        /// The device has encountered an error
        /// </summary>
        Error,

        /// <summary>
        /// The device is unresponsive
        /// </summary>
        Unresponsive
    }
}
