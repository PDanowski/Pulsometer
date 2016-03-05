using System;
using System.Collections.Generic;
using Android.Content;
using Android.Hardware;
using Pulsometer.ViewModel.Interfaces;

namespace Pulsometer.Services
{
    public class HRMSensorService : ISensorEventListener
    {
        private readonly Context context;

        public HRMSensorService(Context context)
        {
            this.context = context;
        }

        public void FindHRMSensore()
        {
            var sensorManager = (SensorManager) context.GetSystemService(Context.SensorService);

            var heartRateSensor = sensorManager.GetDefaultSensor(SensorType.HeartRate);

            if (heartRateSensor != null)
            {
               sensorManager.RegisterListener(this, heartRateSensor, SensorDelay.Ui);
            }
            
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IntPtr Handle { get; }

        public void OnAccuracyChanged(Sensor sensor, SensorStatus accuracy)
        {
            throw new NotImplementedException();
        }

        public void OnSensorChanged(SensorEvent e)
        {
            throw new NotImplementedException();
        }
    }
}