using System;
using Pulsometer.Model.SQLiteConnection;

namespace Pulsometer.Model.Models
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
        public float Value { get; set; }
        public string Note { get; set; }
        public DateTime Date { get; set; }
        public Conditions Condition { get; set; }
    }
}
