using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Pulsometer.Model.Models;
using Pulsometer.Model.SQLiteConnection;
using Pulsometer.ViewModel.Interfaces;

namespace Pulsometer.ViewModel.ViewModels
{
    public class LastMeasurementsViewModel
    {
        private readonly ILastMeasurementsViewAccess access;
        private readonly ISQLiteConnector sqLiteConnector;

        private List<Measurement> lastMeasurements;

        public LastMeasurementsViewModel(ILastMeasurementsViewAccess access, ISQLiteConnector sqLiteConnector)
        {
            this.access = access;
            this.sqLiteConnector = sqLiteConnector;

            lastMeasurements = new List<Measurement>();
        }

        public void LoadMeasurements()
        {
            lastMeasurements = sqLiteConnector.SelectLastMeasurementsAsync(10);
        }

        public List<float> GetLastMeasurements()
        {
            return lastMeasurements.Select(element => element.Value).ToList();
        }
    }
}
