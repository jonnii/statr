namespace Statr.Routing
{
    public class AccumulateAggregationStrategy : IAggregationStrategy
    {
        public AggregatedMetric Current { get; private set; }

        public AggregatedMetric Aggregate(AggregatedMetric original, Metric metric)
        {
            var countMetric = metric;

            Current = new AggregatedMetric
            {
                LastValue = countMetric.Value,
                NumMetrics = ++original.NumMetrics,
                Value = original.Value + countMetric.Value
            };

            return Current;
        }
    }
}