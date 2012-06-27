namespace Statr
{
    public class GaugeMetric : Metric
    {
        public GaugeMetric(string name, float amount)
            : base(name)
        {
            Amount = amount;
        }

        public float Amount { get; set; }

        public override string ToString()
        {
            return string.Concat("[Gauge Name=", Name, ", Amount=", Amount, "]");
        }
    }
}