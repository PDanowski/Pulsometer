using System;
using System.Collections.Generic;
using System.Linq;
using Pulsometer.Model.Models;
using Pulsometer.Model.Models.Enums;
using Pulsometer.Model.SQLiteConnection;
using Pulsometer.Model.XMLSerialization;
using Pulsometer.ViewModel.Interfaces;

namespace Pulsometer.ViewModel.ViewModels
{
    public class MainViewModel
    {
        private readonly IMainViewAccess access;
        private readonly IProperPulseRangeCounter properPulseRangeCounter;
        private readonly ISQLiteConnector sqLiteConnector;
        private IUserConfiguration config;
        private IList<SingleMeasurement> singleMeasurements;

        public MainViewModel(IMainViewAccess access, ISQLiteConnector sqLiteConnector,
            IProperPulseRangeCounter properPulseRangeCounter)
        {
            this.access = access;
            this.properPulseRangeCounter = properPulseRangeCounter;
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
            singleMeasurements = new List<SingleMeasurement>();

            access.DisplayProgressDialog();
            access.RegisterHRMSensore();
        }

        public void StopMeasure()
        {
            access.CloseProgressDialog();
            access.DisplayCompletedMeasureDialog(singleMeasurements.Average(m => m.Value));
            access.UnregisterHRMSensore();
        }

        public void SaveMeasurement(string note, State state)
        {
            var measurementValue = singleMeasurements.Average(m => m.Value);

            var measurement = new Measurement
            {
                Value = measurementValue,
                Date = DateTime.Now,
                Note = note,
                State = state
            };

            sqLiteConnector.InsertAsync(measurement);

            access.DisplaySuccessfullSavedDataMessage();
        }

        public void SaveUserConfiguration()
        {
            UserSerializer.Serialize(config);
        }

        public void SetUserConfiguration(string name, DateTime birthday, Gender gender)
        {
            config.Name = name;
            config.Birthday = birthday;
            config.Gender = gender;
        }

        public void LoadUserConfiguration()
        {
            config = UserSerializer.Deserialize();

            if (config == null)
            {
                config = new UserConfiguration();
                access.DisplayWelcomeDialog();
            }

            access.SetUserConfig(config);
        }

        public event EventHandler<EventArgs> ListReachedTargetEvent;

        public Range GetAverageRange()
        {
            return properPulseRangeCounter.GetAverageRange(config);
        }

        public Range GetFullRange()
        {
            return properPulseRangeCounter.GetFullRange(config);
        }
    }
}