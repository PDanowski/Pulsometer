using System;
using System.Collections.Generic;
using Pulsometer.Model.Models;

namespace Pulsometer.Model.SQLiteConnection
{
    public interface ISQLiteConnector
    {
        void CreateTable();
        void Insert(Measurement measurement);
        void Update(Measurement measurement);
        void Delete(Measurement measurement);
        List<Measurement> SelectAll();
        Measurement SelectFirstOrDefault(DateTime date);
    }
}
