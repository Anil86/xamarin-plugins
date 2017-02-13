using System;

namespace DeviceMotion.Plugin.Abstractions
{
	/// <summary>
	/// Motion sensor type enum.
	/// </summary>
	public enum MotionSensorType
	{
        /// <summary>
        /// Accelerometer Sensor
        /// </summary>
		Accelerometer,
        /// <summary>
        /// Gyroscope Sensor
        /// </summary>
		Gyroscope,
        /// <summary>
        /// Magnetometer Sensor
        /// </summary>
		Magnetometer,
        /// <summary>
        /// Compass Sensor
        /// </summary>
        Compass,
        /// <summary>
        /// Step detector Sensor
        /// </summary>
        StepDetector,
        /// <summary>
        /// Step counter Sensor
        /// </summary>
        StepCounter
    }
}

