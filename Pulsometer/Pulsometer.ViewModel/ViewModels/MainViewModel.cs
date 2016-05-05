using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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
        private readonly ISQLiteConnector sqLiteConnector;
        private IList<SingleMeasurement> singleMeasurements;
        private IUserConfiguration config;

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

        public void SaveMeasurement(string note, State? state)
        {
            var measurementValue = singleMeasurements.Average(m => m.Value);

            var measurement = new Measurement()
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

        public void SetUserConfiguration(string name, string age, string gender)
        {
            config.Name = name;
            config.Age = Int32.Parse(age);
            config.Gender = (Gender)Enum.Parse(typeof(Gender), gender);
            config.Notifications = new List<DateTime>();
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
    }
}