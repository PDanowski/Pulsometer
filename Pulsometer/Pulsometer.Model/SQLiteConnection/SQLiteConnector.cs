using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Pulsometer.Model.Models;

namespace Pulsometer.Model.SQLiteConnection
{
    public class SQLiteConnector : ISQLiteConnector
    {
        private readonly SQLiteAsyncConnection connection;
        private const string DatabaseName = "pulsometer.db3";

        public SQLiteConnector()
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), DatabaseName);
            connection = new SQLiteAsyncConnection(dbPath);
        }

        public async void CreateTableAsync()
        {
            await connection.CreateTableAsync<Measurement>();
        }

        public async void InsertAsync(Measurement measurement)
        {
            await connection.InsertAsync(measurement);
        }

        public async void UpdateAsync(Measurement measurement)
        {
            await connection.UpdateAsync(measurement);
        }

        public async void DeleteAsync(Measurement measurement)
        {
            await connection.DeleteAsync(measurement);
        }

        public async Task<List<Measurement>> SelectAllAsync()
        {
            var data = await connection.Table<Measurement>().ToListAsync();

            return data;
        }

        public List<Measurement> SelectAllSync()
        {
            var data = connection.Table<Measurement>().ToListAsync().Result;

            return data;
        }

        public List<Measurement> SelectLastMeasurementsAsync(int n)
        {
            var data = connection.Table<Measurement>().OrderByDescending(x => x.Date).Take(n).ToListAsync().Result;

            return data;
        }

        public async Task<List<Measurement>> SelectAllByDateAsync(DateTime date)
        {
            var oneDayLaterDate = date.AddDays(1);
            var data = await connection.Table<Measurement>()
                .Where(m => m.Date >= date)
                .Where(m => m.Date < oneDayLaterDate)
                .ToListAsync();

            return data;
        }

        public async Task<Measurement> SelectFirstOrDefaultAsync()
        {
            var data = await connection.Table<Measurement>().FirstOrDefaultAsync();

            return data;
        }
    }
}
