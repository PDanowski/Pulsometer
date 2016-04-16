using System;
using System.Collections.Generic;
using System.Linq;
using Pulsometer.Model.Models;
using Pulsometer.Model.SQLiteConnection;
using Pulsometer.ViewModel.Interfaces;

namespace Pulsometer.ViewModel.ViewModels
{
    public class CalendarViewModel
    {
        private readonly ICalendarViewAccess access;
        private readonly ISQLiteConnector sqLiteConnector;
        private List<Measurement> measurements;

        public CalendarViewModel(ICalendarViewAccess access, ISQLiteConnector sqLiteConnector)
        {
            this.access = access;
            this.sqLiteConnector = sqLiteConnector;
        }

        public void InfrastructureReady()
        {
            FetchMeasurements();
            SetMinDate();
            access.SetCalendarModels(measurements);
        }

        private void FetchMeasurements()
        {
            measurements = sqLiteConnector.SelectAll();
        }

        private void SetMinDate()
        {
            var firstOrDefaultMeasurement = measurements.FirstOrDefault();
            if (firstOrDefaultMeasurement != null)
            {
                access.SetMinDate(firstOrDefaultMeasurement.Date);
            }
        }

        public void OnCalendarSelectedDate(DateTime selectedDate)
        {
            var measurementsOfSelectedDate = measurements
                .Where(m =>
                    m.Date.Day == selectedDate.Day &&
                    m.Date.Month == selectedDate.Month &&
                    m.Date.Year == selectedDate.Year)
                .ToList();

            access.OpenWindowWithSelectedMeasurements(measurementsOfSelectedDate);
        }
    }
}
