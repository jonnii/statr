namespace Statr
{
    public class CountMetric : Metric
    {
        public CountMetric(string name, float amount)
            : base(name)
        {
            Amount = amount;
        }

        public float Amount { get; set; }

        public override string ToString()
        {
            return string.Concat("[CountMetric Name=", Name, ", Amount=", Amount, "]");
        }
    }
}