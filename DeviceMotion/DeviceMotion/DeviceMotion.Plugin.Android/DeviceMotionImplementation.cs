using DeviceMotion.Plugin.Abstractions;
using Android.Hardware;
using Android.Content;
using Android.App;
using Android.Runtime;
using System;
using System.Collections.Generic;


namespace DeviceMotion.Plugin
{
    /// <summary>
    /// Implementation for Feature
    /// </summary>
    public class DeviceMotionImplementation : Java.Lang.Object, ISensorEventListener, IDeviceMotion
    {
        private readonly SensorManager _sensorManager;
        private readonly Sensor _sensorAccelerometer;
        private readonly Sensor _sensorGyroscope;
        private readonly Sensor _sensorMagnetometer;
        private readonly Sensor _sensorCompass;
        private readonly Sensor _sensorStepDetector;
        private readonly Sensor _sensorStepCounter;

        private readonly IDictionary<MotionSensorType, bool> _sensorStatus;

        /// <summary>
        /// Initializes a new instance of the DeviceMotionImplementation class.
        /// </summary>
        public DeviceMotionImplementation() : base()
        {

            _sensorManager = (SensorManager)Application.Context.GetSystemService(Context.SensorService);
            _sensorAccelerometer = _sensorManager.GetDefaultSensor(SensorType.Accelerometer);
            _sensorGyroscope = _sensorManager.GetDefaultSensor(SensorType.Gyroscope);
            _sensorMagnetometer = _sensorManager.GetDefaultSensor(SensorType.MagneticField);
            _sensorCompass = _sensorManager.GetDefaultSensor(SensorType.Orientation);
            _sensorStepDetector = _sensorManager.GetDefaultSensor(SensorType.StepDetector);
            _sensorStepCounter = _sensorManager.GetDefaultSensor(SensorType.StepCounter);
            _sensorStatus = new Dictionary<MotionSensorType, bool>
            {
                {MotionSensorType.Accelerometer, false},
                {MotionSensorType.Gyroscope, false},
                {MotionSensorType.Magnetometer, false},
                {MotionSensorType.Compass, false},
                {MotionSensorType.StepDetector, false},
                {MotionSensorType.StepCounter, false}
            };
        }

        /// <summary>
        /// Occurs when sensor value changed.
        /// </summary>
        public event SensorValueChangedEventHandler SensorValueChanged;

        /// <Docs>To be added.</Docs>
        /// <summary>
        /// Called when the accuracy of a sensor has changed.
        /// </summary>
        /// <para tool="javadoc-to-mdoc">Called when the accuracy of a sensor has changed.</para>
        /// <param name="sensor">Sensor.</param>
        /// <param name="accuracy">Accuracy.</param>
        public void OnAccuracyChanged(Sensor sensor, SensorStatus accuracy)
        {

        }

        /// <summary>
        /// Raises the sensor changed event.
        /// </summary>
        /// <param name="e">E.</param>
        public void OnSensorChanged(SensorEvent e)
        {
            if (SensorValueChanged == null)
                return;


            switch (e.Sensor.Type)
            {
                case SensorType.Accelerometer:
                    SensorValueChanged(this, new SensorValueChangedEventArgs() { ValueType = MotionSensorValueType.Vector, SensorType = MotionSensorType.Accelerometer, Value = new MotionVector() { X = e.Values[0], Y = e.Values[1], Z = e.Values[2] }, Timestamp = e.Timestamp });

                    break;
                case SensorType.Gyroscope:
                    SensorValueChanged(this, new SensorValueChangedEventArgs() { ValueType = MotionSensorValueType.Vector, SensorType = MotionSensorType.Gyroscope, Value = new MotionVector() { X = e.Values[0], Y = e.Values[1], Z = e.Values[2] }, Timestamp = e.Timestamp });

                    break;
                case SensorType.MagneticField:
                    SensorValueChanged(this, new SensorValueChangedEventArgs() { ValueType = MotionSensorValueType.Vector, SensorType = MotionSensorType.Magnetometer, Value = new MotionVector() { X = e.Values[0], Y = e.Values[1], Z = e.Values[2] }, Timestamp = e.Timestamp });

                    break;
                case SensorType.Orientation:
                    SensorValueChanged(this, new SensorValueChangedEventArgs() { ValueType = MotionSensorValueType.Single, SensorType = MotionSensorType.Compass, Value = new MotionValue() { Value = e.Values[0] }, Timestamp = e.Timestamp });
                    break;

                case SensorType.StepDetector:
                    SensorValueChanged(this, new SensorValueChangedEventArgs { ValueType = MotionSensorValueType.Single, SensorType = MotionSensorType.StepDetector, Value = new MotionValue { Value = e.Values[0] }, Timestamp = e.Timestamp });
                    break;

                case SensorType.StepCounter:
                    SensorValueChanged(this, new SensorValueChangedEventArgs { ValueType = MotionSensorValueType.Single, SensorType = MotionSensorType.StepCounter, Value = new MotionValue { Value = e.Values[0] }, Timestamp = e.Timestamp });
                    break;
            }
        }




        /// <summary>
        /// Start the specified sensorType and interval.
        /// </summary>
        /// <param name="sensorType">Sensor type.</param>
        /// <param name="interval">Interval.</param>
        public void Start(MotionSensorType sensorType, MotionSensorDelay interval = MotionSensorDelay.Default)
        {


            SensorDelay delay = SensorDelay.Normal;
            switch (interval)
            {
                case MotionSensorDelay.Fastest:
                    delay = SensorDelay.Fastest;
                    break;
                case MotionSensorDelay.Game:
                    delay = SensorDelay.Game;
                    break;
                case MotionSensorDelay.Ui:
                    delay = SensorDelay.Ui;
                    break;

            }
            switch (sensorType)
            {
                case MotionSensorType.Accelerometer:
                    if (_sensorAccelerometer != null)
                        _sensorManager.RegisterListener(this, _sensorAccelerometer, delay);
                    else
                        Console.WriteLine("Accelerometer not available");
                    break;
                case MotionSensorType.Gyroscope:
                    if (_sensorGyroscope != null)
                        _sensorManager.RegisterListener(this, _sensorGyroscope, delay);
                    else
                        Console.WriteLine("Gyroscope not available");
                    break;
                case MotionSensorType.Magnetometer:
                    if (_sensorMagnetometer != null)
                        _sensorManager.RegisterListener(this, _sensorMagnetometer, delay);
                    else
                        Console.WriteLine("Magnetometer not available");
                    break;
                case MotionSensorType.Compass:
                    if (_sensorCompass != null)
                        _sensorManager.RegisterListener(this, _sensorCompass, delay);
                    else
                        Console.WriteLine("Compass not available");
                    break;

                case MotionSensorType.StepDetector:
                    if (_sensorStepDetector != null)
                        _sensorManager.RegisterListener(this, _sensorStepDetector, delay);
                    else
                        Console.WriteLine("Step Detector is not available");
                    break;
                case MotionSensorType.StepCounter:
                    if (_sensorStepCounter != null)
                        _sensorManager.RegisterListener(this, _sensorStepCounter, delay);
                    else
                        Console.WriteLine("Sensor Counter is not available");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(sensorType), sensorType, null);
            }
            _sensorStatus[sensorType] = true;

        }

        /// <summary>
        /// Stop the specified sensorType.
        /// </summary>
        /// <param name="sensorType">Sensor type.</param>
        public void Stop(MotionSensorType sensorType)
        {
            switch (sensorType)
            {
                case MotionSensorType.Accelerometer:
                    if (_sensorAccelerometer != null)
                        _sensorManager.UnregisterListener(this, _sensorAccelerometer);
                    else
                        Console.WriteLine("Accelerometer not available");
                    break;
                case MotionSensorType.Gyroscope:
                    if (_sensorGyroscope != null)
                        _sensorManager.UnregisterListener(this, _sensorGyroscope);
                    else
                        Console.WriteLine("Gyroscope not available");
                    break;
                case MotionSensorType.Magnetometer:
                    if (_sensorMagnetometer != null)
                        _sensorManager.UnregisterListener(this, _sensorMagnetometer);
                    else
                        Console.WriteLine("Magnetometer not available");
                    break;
                case MotionSensorType.Compass:
                    if (_sensorCompass != null)
                        _sensorManager.UnregisterListener(this, _sensorCompass);
                    else
                        Console.WriteLine("Compass not available");
                    break;
                case MotionSensorType.StepDetector:
                    if (_sensorStepDetector != null)
                        _sensorManager.UnregisterListener(this, _sensorStepDetector);
                    else
                        Console.WriteLine("Step Detector is not available");
                    break;
                case MotionSensorType.StepCounter:
                    if (_sensorStepCounter != null)
                        _sensorManager.UnregisterListener(this, _sensorStepCounter);
                    else
                        Console.WriteLine("Step Counter is not available");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(sensorType), sensorType, null);
            }
            _sensorStatus[sensorType] = false;
        }

        /// <summary>
        /// Determines whether this instance is active the specified sensorType.
        /// </summary>
        /// <returns><c>true</c> if this instance is active the specified sensorType; otherwise, <c>false</c>.</returns>
        /// <param name="sensorType">Sensor type.</param>
        public bool IsActive(MotionSensorType sensorType)
        {
            return _sensorStatus[sensorType];
        }

    }
}