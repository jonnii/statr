using System;

namespace Statr
{
    public class MetricParser : IMetricParser
    {
        public Metric Parse(string raw)
        {
            var colon = raw.IndexOf(":", StringComparison.Ordinal);
            var name = raw.Substring(0, colon);

            if (raw.EndsWith("c"))
            {
                var pipe = raw.IndexOf("|", StringComparison.Ordinal);
                var value = raw.Substring(colon + 1, pipe - colon - 1);

                return new Metric(name, Single.Parse(value), MetricType.Count);
            }

            if (raw.EndsWith("g"))
            {
                var pipe = raw.IndexOf("|", StringComparison.Ordinal);
                var value = raw.Substring(colon + 1, pipe - colon - 1);

                return new Metric(name, Single.Parse(value), MetricType.Gauge);
            }

            throw new NotSupportedException("metric not yet supported");
        }
    }
}