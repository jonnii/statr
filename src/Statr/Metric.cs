using System;

namespace Statr
{
    public abstract class Metric
    {
        public static Metric Parse(string raw)
        {
            var colon = raw.IndexOf(":", StringComparison.Ordinal);
            var name = raw.Substring(0, colon);

            if (raw.EndsWith("c"))
            {
                var pipe = raw.IndexOf("|", StringComparison.Ordinal);
                var amount = raw.Substring(colon + 1, pipe - colon - 1);

                return new CountMetric(name, long.Parse(amount));
            }

            throw new NotSupportedException("metric not yet supported");
        }

        protected Metric(string name)
        {
            Name = name;
            TimeStamp = DateTime.UtcNow;
        }

        public DateTime TimeStamp { get; private set; }

        public string Name { get; private set; }
    }
}