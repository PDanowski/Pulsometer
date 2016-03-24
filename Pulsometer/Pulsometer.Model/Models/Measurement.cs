using System;
using System.Collections.Generic;
using System.Linq;
using Pulsometer.Model.SQLiteConnection;

namespace Pulsometer.Model
{
    public enum Conditions
    {
        //TODO 
    }

    [Table("Measurements")]
    public class Measurement
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int Pulse { get; set; }
        public string Note { get; set; }
        public DateTime Date { get; set; }
        public Conditions Condition { get; set; }
    }
}
