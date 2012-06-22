namespace Statr
{
    public class CountMetric : Metric
    {
        public CountMetric(string name, long amount)
            : base(name)
        {
            Amount = amount;
        }

        public long Amount { get; set; }

        public override string ToString()
        {
            return string.Concat("[CountMetric Name=", Name, ", Amount=", Amount, "]");
        }
    }
}