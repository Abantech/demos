using System;
using System.Collections.Generic;

namespace HandJointsMeasurement
{
    /// <summary>
    /// Abstraction of the capture device
    /// </summary>
    public interface ICaptureDevice : IDisposable
    {
        /// <summary>
        /// Initializes the capture device
        /// </summary>
        /// <returns>The status of the device</returns>
        DeviceStatus Initialize();

        /// <summary>
        /// Provides a unique identifier for the device to distinguish it from others/several.
        /// </summary>
        string DeviceID { get; }

        /// <summary>
        /// Gets the most recent unprocessed frame.
        /// </summary>
        /// <returns></returns>
        CaptureFrame GetFrame();

        IEnumerable<CaptureFrame> GetHistoricalFrames(int count);
    }
}
