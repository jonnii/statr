namespace Statr.Routing
{
    public class AccumulateAggregationStrategy : IAggregationStrategy
    {
        public AggregatedMetric Aggregate(AggregatedMetric original, Metric metric)
        {
            var countMetric = metric;

            return new AggregatedMetric
            {
                LastValue = countMetric.Value,
                NumMetrics = ++original.NumMetrics,
                Value = original.Value + countMetric.Value
            };
        }
    }
}