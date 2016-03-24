using System;
using System.IO;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Hardware;
using Android.OS;
using Android.Util;
using Android.Widget;
using Pulsometer.Dependencies;
using Pulsometer.ViewModel.Interfaces;
using Pulsometer.ViewModel.ViewModels;

namespace Pulsometer.Activities
{
    [Activity(Label = "Pulsometer", MainLauncher = true, Icon = "@drawable/icon", 
        LaunchMode = LaunchMode.SingleTask, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : Activity, IMainViewAccess, ISensorEventListener
    {
        private readonly MainViewModel viewModel;
        private TextView heartRate;

        public MainActivity()
        {
            var viewModelFactory = Container.Resolve<IViewModelsFactory>();
            this.viewModel = viewModelFactory.GetMainViewModel(this);
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            InitializeObjects();
        }

        private void InitializeObjects()
        {
            heartRate = FindViewById<TextView>(Resource.Id.HeartRate);
            var button = FindViewById<Button>(Resource.Id.MyButton);
            button.Click += (sender, args) => HRMSensorEmulator();
            button.Click += (sender, args) => FindHRMSensore();
        }

        private async void HRMSensorEmulator()
        {
            Random rnd = new Random();

            while (true)
            {
                await Task.Delay(500);
                Log.Debug("HRM_emulator", $"Value: {rnd.Next(50, 120)}");
            }
        }

        public void FindHRMSensore()
        {
            var sensorManager = (SensorManager)this.GetSystemService(Context.SensorService);

            var heartRateSensor = sensorManager.GetDefaultSensor(SensorType.HeartRate);

            if (heartRateSensor != null)
            {
                sensorManager.RegisterListener(this, heartRateSensor, SensorDelay.Fastest);
            }

        }

        public void OnAccuracyChanged(Sensor sensor, SensorStatus accuracy)
        {
            
        }

        public void OnSensorChanged(SensorEvent e)
        {
            RunOnUiThread( () => heartRate.Text = e.Values[0].ToString() );
            
            Log.Debug("TAG", $"Value: {e.Values[0]}, Accuracy: {e.Accuracy}");
        }
    }
}

