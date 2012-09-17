namespace Statr.Server.Routing
{
    public class LastValueAggregationStrategy : IAggregationStrategy
    {
        public AggregatedMetric Aggregate(AggregatedMetric original, Metric metric)
        {
            var countMetric = metric;

            return new AggregatedMetric
            {
                LastValue = countMetric.Value,
                NumMetrics = ++original.NumMetrics,
                Value = countMetric.Value
            };
        }
    }
}