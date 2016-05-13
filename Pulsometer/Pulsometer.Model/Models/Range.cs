using System;

namespace Pulsometer.Model.Models
{
    public class Range
    {
        public int Lower { get; set; }
        public int Upper { get; set; }

        public Range(int lower, int upper)
        {
            this.Lower = lower;
            this.Upper = upper;
        }

        public bool IsProper(float heartRateValue)
        {
            return heartRateValue > Lower && heartRateValue < Upper;
        }
    }
}