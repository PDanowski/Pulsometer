using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Widget;
using Pulsometer.Dependencies;
using Pulsometer.Services;
using Pulsometer.ViewModel.Interfaces;
using Pulsometer.ViewModel.ViewModels;

namespace Pulsometer.Activities
{
    [Activity(Label = "Pulsometer", MainLauncher = true, Icon = "@drawable/icon", 
        LaunchMode = LaunchMode.SingleTask, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : Activity, IMainViewAccess
    {
        private readonly MainViewModel viewModel;
        private readonly HRMSensorService hrmSensorService;

        public MainActivity()
        {
            var viewModelFactory = Container.Resolve<IViewModelsFactory>();
            this.viewModel = viewModelFactory.GetMainViewModel(this);
            hrmSensorService = new HRMSensorService(this);
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            InitializeObjects();
        }

        private void InitializeObjects()
        {
            Button button = FindViewById<Button>(Resource.Id.MyButton);
            button.Click += (sender, args) => viewModel.OnButtonClick();
        }

        void IMainViewAccess.StartHRMSensor()
        {
            hrmSensorService.FindHRMSensore();
        }
    }
}

