namespace Statr.Api.Models
{
    public class DataPoint
    {
        public long TimeStamp { get; set; }

        public uint NumMetrics { get; set; }

        public float Value { get; set; }
    }
}
