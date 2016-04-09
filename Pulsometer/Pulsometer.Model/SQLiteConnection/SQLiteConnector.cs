using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Pulsometer.Model.Models;

namespace Pulsometer.Model.SQLiteConnection
{
    public class SQLiteConnector : ISQLiteConnector
    {
        private readonly SQLiteAsyncConnection connection;

        public SQLiteConnector()
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal),"pulsometer.db3");
            connection = new SQLiteAsyncConnection(dbPath);
        }

        public void CreateTable()
        {
            connection.CreateTableAsync<Measurement>().ContinueWith(results =>
            {
                Debug.WriteLine("Table created");
            });
        }

        public void Insert(Measurement measurement)
        {
            connection.InsertAsync(measurement).ContinueWith(results =>
            {
                Debug.WriteLine("New measurement ID: {0}", measurement.Id);
            });
        }

        public void Update(Measurement measurement)
        {
            connection.UpdateAsync(measurement).ContinueWith(results =>
            {
                Debug.WriteLine("Updated measurement ID: {0}", measurement.Id);
            });
        }

        public void Delete(Measurement measurement)
        {
            connection.DeleteAsync(measurement).ContinueWith(results =>
            {
                Debug.WriteLine("Deleted measurement ID: {0}", measurement.Id);
            });
        }

        public List<Measurement> SelectAll()
        {
            var query = connection.Table<Measurement>();

            return query.ToListAsync().Result;
        }

        public Measurement SelectFirstOrDefault(DateTime date)
        {
            var query = connection.Table<Measurement>().Where(x => x.Date.Equals(date));

            return query.FirstOrDefaultAsync().Result;
        }

    }
}
