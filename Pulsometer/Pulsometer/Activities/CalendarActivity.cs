using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Widget;
using com.alliance.calendar;
using Java.Util;
using Pulsometer.Dependencies;
using Pulsometer.Model.Models;
using Pulsometer.ViewModel.Interfaces;
using Pulsometer.ViewModel.ViewModels;

namespace Pulsometer.Activities
{
    [Activity(Label = "Calendar", Theme = "@style/MyTheme", ScreenOrientation = ScreenOrientation.Portrait)]
    public class CalendarActivity : Activity, ICalendarViewAccess
    {
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

            viewModel.InfrastructureReady();
        }

        private void InitializeObjects()
        {
            calendar = FindViewById<CustomCalendar>(Resource.Id.calendar);
            calendar.NextButtonText = Resources.GetString(Resource.String.next);
            calendar.PreviousButtonText = Resources.GetString(Resource.String.previous);
            calendar.OnCalendarSelectedDate += CalendarOnOnCalendarSelectedDate;
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

        }
    }
}