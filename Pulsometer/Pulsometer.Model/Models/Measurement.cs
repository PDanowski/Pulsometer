using System;
using Pulsometer.Model.Models.Enums;
using Pulsometer.Model.SQLiteConnection;

namespace Pulsometer.Model.Models
{
    [Table("Measurements")]
    public class Measurement
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public float Value { get; set; }
        public string Note { get; set; }
        public DateTime Date { get; set; }
        public State? State { get; set; }
    }
}
