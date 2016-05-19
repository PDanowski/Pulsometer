using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using com.alliance.calendar;
using Pulsometer.Dependencies;
using Pulsometer.Dialogs;
using Pulsometer.Model.Models;
using Pulsometer.ViewModel.Interfaces;
using Pulsometer.ViewModel.ViewModels;
using SupportToolbar = Android.Support.V7.Widget.Toolbar;

namespace Pulsometer.Activities
{
    [Activity(Label = "@string/calendarItem", Theme = "@style/MyTheme", ScreenOrientation = ScreenOrientation.Portrait)]
    public class CalendarActivity : AppCompatActivity, ICalendarViewAccess
    {
        private SupportToolbar toolbar;
        private CustomCalendar calendar;
        private readonly CalendarViewModel viewModel;

        public CalendarActivity()
        {
            var viewModelFactory = Container.Resolve<IViewModelsFactory>();
            viewModel = viewModelFactory.GetCalendarViewModel(this);
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Calendar);

            InitializeObjects();
            SetUpToolbar();

            viewModel.InfrastructureReady();
        }

        private void SetUpToolbar()
        {
            SetSupportActionBar(toolbar);

            SupportActionBar.SetHomeButtonEnabled(true);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
        }

        private void InitializeObjects()
        {
            calendar = FindViewById<CustomCalendar>(Resource.Id.calendar);
            calendar.NextButtonText = Resources.GetString(Resource.String.next);
            calendar.PreviousButtonText = Resources.GetString(Resource.String.previous);
            calendar.OnCalendarSelectedDate += CalendarOnOnCalendarSelectedDate;

            toolbar = FindViewById<SupportToolbar>(Resource.Id.toolbar);

        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            Finish();
            return base.OnOptionsItemSelected(item);
        }

        private void CalendarOnOnCalendarSelectedDate(object sender, CalendarDateSelectionEventArgs eventArgs)
        {
            viewModel.OnCalendarSelectedDate(eventArgs.SelectedDate);
        }

        public void SetMinDate(DateTime minDate)
        {
            calendar.ShowFromDate = minDate;
        }

        public void SetCalendarModels(List<Measurement> measurements)
        {
            var customData = measurements.Select(measurement => new CustomCalendarData(measurement.Date)).ToList();

            calendar.CustomDataAdapter = customData;
        }

        public void OpenWindowWithSelectedMeasurements(List<Measurement> measurements)
        {
            if (measurements.Count == 0)
            {
                HandleEmptyMeasurementList();
                return;
            }

            var dialog = new MeasuresOfDayDialog(this, LayoutInflater, viewModel);

            dialog.Show(measurements);
        }

        private void HandleEmptyMeasurementList()
        {
            var handler = new Handler(Looper.MainLooper);
            handler.Post(() => Toast.MakeText(this, Resources.GetString(Resource.String.lackOfMeasurements), ToastLength.Long).Show());
        }
    }
}