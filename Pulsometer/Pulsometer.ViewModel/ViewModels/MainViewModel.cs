using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pulsometer.Model.Models;
using Pulsometer.Model.SQLiteConnection;
using Pulsometer.ViewModel.Interfaces;

namespace Pulsometer.ViewModel.ViewModels
{
    public class MainViewModel
    {
        private readonly IMainViewAccess access;
        private readonly ISQLiteConnector sqLiteConnector;
        private IList<SingleMeasurement> singleMeasurements;

        public MainViewModel(IMainViewAccess access, ISQLiteConnector sqLiteConnector)
        {
            this.access = access;
            this.sqLiteConnector = new SQLiteConnector();
            sqLiteConnector.CreateTableAsync();
        }

        public void RegisterSingleMeasurement(float measurement)
        {
            singleMeasurements.Add(new SingleMeasurement(measurement));

            if (singleMeasurements.Count > Constans.ListTarget)
            {
                ListReachedTargetEvent?.Invoke(this, EventArgs.Empty);
            }
        }

        public void StartMeasure()
        {
            this.singleMeasurements = new List<SingleMeasurement>();

            access.DisplayProgressDialog();
            access.RegisterHRMSensore();
        }

        public void StopMeasure()
        {
            access.CloseProgressDialog();
            access.DisplayCompletedMeasureDialog(singleMeasurements.Average(m => m.Value));
            access.UnregisterHRMSensore();
        }

        public void SaveMeasurement(string note)
        {
            var measurementValue = singleMeasurements.Average(m => m.Value);

            var measurement = new Measurement()
            {
                Value = measurementValue,
                Date = DateTime.Now, 
                Note = note
            };

            sqLiteConnector.InsertAsync(measurement);

            access.DisplaySuccessfullSavedDataMessage();
        }

        public event EventHandler<EventArgs> ListReachedTargetEvent;
    }
}