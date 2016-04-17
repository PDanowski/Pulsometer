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

        public CalendarViewModel(ICalendarViewAccess access, ISQLiteConnector sqLiteConnector)
        {
            this.access = access;
            this.sqLiteConnector = sqLiteConnector;
        }

        public async void InfrastructureReady()
        {
            SetMinDate();
            var measurements = await sqLiteConnector.SelectAllAsync();
            access.SetCalendarModels(measurements);
        }

        private async void SetMinDate()
        {
            var firstOrDefaultMeasurement = await sqLiteConnector.SelectFirstOrDefaultAsync();
            if (firstOrDefaultMeasurement != null)
            {
                access.SetMinDate(firstOrDefaultMeasurement.Date);
            }
        }

        public async void OnCalendarSelectedDate(DateTime selectedDate)
        {
            var measurementsOfSelectedDate = await sqLiteConnector.SelectAllByDateAsync(selectedDate);

            access.OpenWindowWithSelectedMeasurements(measurementsOfSelectedDate);
        }
    }
}
