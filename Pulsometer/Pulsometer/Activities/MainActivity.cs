using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Hardware;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using Pulsometer.Dependencies;
using Pulsometer.ViewModel.Interfaces;
using Pulsometer.ViewModel.ViewModels;
using SupportToolbar = Android.Support.V7.Widget.Toolbar;

namespace Pulsometer.Activities
{
    [Activity(Label = "Pulsometer", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/MyTheme",
        LaunchMode = LaunchMode.SingleTask, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : AppCompatActivity, IMainViewAccess, ISensorEventListener
    {
        private readonly MainViewModel viewModel;
        private TextView heartRate;
        private SupportToolbar toolbar;
        private DrawerLayout mainDrawer;
        private NavigationView navigationView;
        private ActionBarDrawerToggle myActionBarDrawerToggle;
        private Button measureHeartRateButton;

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
            SetSupportActionBar(toolbar);
            SetUpToolbar();
        }

        private void SetUpToolbar()
        {
            mainDrawer.SetDrawerListener(myActionBarDrawerToggle);
            SupportActionBar.SetHomeButtonEnabled(true);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            myActionBarDrawerToggle.SyncState();
        }

        private void InitializeObjects()
        {
            heartRate = FindViewById<TextView>(Resource.Id.HeartRate);
            toolbar = FindViewById<SupportToolbar>(Resource.Id.toolbar);
            mainDrawer = FindViewById<DrawerLayout>(Resource.Id.drawerLayout);
            navigationView = FindViewById<NavigationView>(Resource.Id.navigationView);
            measureHeartRateButton = FindViewById<Button>(Resource.Id.MeasureHeartRate);
            myActionBarDrawerToggle = new ActionBarDrawerToggle(this, mainDrawer, Resource.String.openDrawer, Resource.String.closeDrawer);

            // measureHeartRateButton.Click += (sender, args) => HRMSensorEmulator();

            measureHeartRateButton.Click += (sender, args) => FindHRMSensore();

            navigationView.NavigationItemSelected += HandleNvNavigationNavigationItemSelected;
            navigationView.ItemIconTintList = null;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            myActionBarDrawerToggle.OnOptionsItemSelected(item);
            return base.OnOptionsItemSelected(item);
        }

        private void HandleNvNavigationNavigationItemSelected(object sender, NavigationView.NavigationItemSelectedEventArgs e)
        {
            switch (e.MenuItem.ItemId)
            {
                case Resource.Id.CalendarItem:
                    {
                        Intent intent = new Intent();
                        intent.SetClass(this, typeof(CalendarActivity));
                        StartActivity(intent);
                        break;
                    }
                case Resource.Id.InfoItem:
                    {
                        Intent intent = new Intent();
                        intent.SetClass(this, typeof(InfoActivity));
                        StartActivity(intent);
                        break;
                    }
                case Resource.Id.SettingsItem:
                    {
                        Intent intent = new Intent();
                        intent.SetClass(this, typeof(SettingsActivity));
                        StartActivity(intent);
                        break;
                    }
                default:
                    throw new InvalidOperationException("Unsupported item ID");
            }
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
            RunOnUiThread(() => heartRate.Text = e.Values[0].ToString());

            Log.Debug("TAG", $"Value: {e.Values[0]}, Accuracy: {e.Accuracy}");
        }
    }
}

