using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pulsometer.Model.Models;

namespace Pulsometer.Model.SQLiteConnection
{
    public interface ISQLiteConnector
    {
        void CreateTableAsync();
        void InsertAsync(Measurement measurement);
        void UpdateAsync(Measurement measurement);
        void DeleteAsync(Measurement measurement);
        Task<List<Measurement>> SelectAllAsync();
        Task<List<Measurement>> SelectAllByDateAsync(DateTime date);
        Task<Measurement> SelectFirstOrDefaultAsync();
    }
}
