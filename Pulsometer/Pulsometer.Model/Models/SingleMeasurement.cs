namespace Pulsometer.Model.Models
{
    public class SingleMeasurement
    {
        public SingleMeasurement(float value)
        {
            Value = value;
        }

        public float Value { get; set; }
    }
}