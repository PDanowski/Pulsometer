using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Hardware;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using Pulsometer.Dependencies;
using Pulsometer.Dialogs;
using Pulsometer.ViewModel;
using Pulsometer.ViewModel.Interfaces;
using Pulsometer.ViewModel.ViewModels;
using AlertDialog = Android.App.AlertDialog;
using SupportToolbar = Android.Support.V7.Widget.Toolbar;

namespace Pulsometer.Activities
{
    [Activity(Label = "Pulsometer", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/MyTheme",
        LaunchMode = LaunchMode.SingleTask, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : AppCompatActivity, IMainViewAccess, ISensorEventListener
    {
        private readonly MainViewModel viewModel;
        private SupportToolbar toolbar;
        private DrawerLayout mainDrawer;
        private NavigationView navigationView;
        private ActionBarDrawerToggle myActionBarDrawerToggle;
        private Button measureHeartRateButton;
        private ProgressDialog measureProgress;

        private bool isMeasureTargetReached = false;

        public MainActivity()
        {
            var viewModelFactory = Container.Resolve<IViewModelsFactory>();
            viewModel = viewModelFactory.GetMainViewModel(this);
            viewModel.ListReachedTargetEvent += OnListReachedTargetEvent;
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            InitializeObjects();
            SetSupportActionBar(toolbar);
            SetUpToolbar();
        }

        private void OnListReachedTargetEvent(object sender, EventArgs eventArgs)
        {
            isMeasureTargetReached = true;
            measureProgress.SetTitle(Resources.GetString(Resource.String.release));
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
            toolbar = FindViewById<SupportToolbar>(Resource.Id.toolbar);
            mainDrawer = FindViewById<DrawerLayout>(Resource.Id.drawerLayout);
            navigationView = FindViewById<NavigationView>(Resource.Id.navigationView);
            measureHeartRateButton = FindViewById<Button>(Resource.Id.MeasureHeartRate);
            measureHeartRateButton.Click += MeasureHeartRateButtonOnClick;
            myActionBarDrawerToggle = new ActionBarDrawerToggle(this, mainDrawer, Resource.String.openDrawer, Resource.String.closeDrawer);

            navigationView.NavigationItemSelected += HandleNvNavigationNavigationItemSelected;
            navigationView.ItemIconTintList = null;
        }

        private void MeasureHeartRateButtonOnClick(object sender, EventArgs eventArgs)
        {
            viewModel.StartMeasure();
        }

        public override void OnBackPressed()
        {
            if (mainDrawer.IsDrawerOpen(GravityCompat.Start))
            {
                mainDrawer.CloseDrawers();
                return;
            }
            base.OnBackPressed();
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

        private async void EmulateHrmSensor()
        {
            Random emulator = new Random();

            while (!isMeasureTargetReached) 
            {
                UpdateProgressBar();
                Log.Debug("HRM_emulator", $"Value: {emulator.Next(50, 120)}");
                viewModel.RegisterSingleMeasurement(emulator.Next(50, 120));

                await Task.Delay(1200);
            }

            viewModel.StopMeasure();
        }

        public void OnAccuracyChanged(Sensor sensor, SensorStatus accuracy)
        {

        }

        public void OnSensorChanged(SensorEvent e)
        {
            if (e.Accuracy == SensorStatus.AccuracyHigh)
            {
                UpdateProgressBar();
                viewModel.RegisterSingleMeasurement(e.Values[0]);
            }

            if (e.Accuracy == SensorStatus.Unreliable)
            {
                if (isMeasureTargetReached)
                {
                    viewModel.StopMeasure();
                }
            }

            Log.Debug("HRM", $"Value: {e.Values[0]}, Accuracy: {e.Accuracy}");
        }

        private void UpdateProgressBar()
        {
            RunOnUiThread(() => measureProgress.Progress = measureProgress.Progress + 1);
        }

        ///////////////// IMainViewAccess Implementation /////////////////////////////////////////

        void IMainViewAccess.RegisterHRMSensore()
        {
            isMeasureTargetReached = false;
            var sensorManager = (SensorManager)this.GetSystemService(Context.SensorService);
            var heartRateSensor = sensorManager.GetDefaultSensor(SensorType.HeartRate);
            if (heartRateSensor != null)
            {
                sensorManager.RegisterListener(this, heartRateSensor, SensorDelay.Fastest);
            }
            else
            {
                EmulateHrmSensor();
            }
        }

        void IMainViewAccess.UnregisterHRMSensore()
        {
            var sensorManager = (SensorManager)this.GetSystemService(Context.SensorService);
            sensorManager.UnregisterListener(this);
        }

        void IMainViewAccess.DisplayCompletedMeasureDialog(float measureValue)
        {
            var completedMeasureDialog = new CompletedMeasureDialog(this, LayoutInflater, viewModel);
            completedMeasureDialog.Show();
            completedMeasureDialog.HeartRate = measureValue;
        }

        void IMainViewAccess.DisplayProgressDialog()
        {
            measureProgress = new ProgressDialog(this);
            measureProgress.SetMessage(Resources.GetString(Resource.String.measuringPulse));
            measureProgress.SetProgressStyle(ProgressDialogStyle.Horizontal);
            measureProgress.Max = Constans.ListTarget;
            measureProgress.Show();
        }

        void IMainViewAccess.CloseProgressDialog()
        {
            measureProgress.Dismiss();
        }

        void IMainViewAccess.DisplaySuccessfullSavedDataMessage()
        {
            var handler = new Handler(Looper.MainLooper);
            handler.Post(() => Toast.MakeText(this, Resources.GetString(Resource.String.successfullSavedData), ToastLength.Long).Show());

        }
    }
}

