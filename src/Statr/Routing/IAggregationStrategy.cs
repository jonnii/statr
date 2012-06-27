namespace Statr.Routing
{
    public interface IAggregationStrategy
    {
        AggregatedMetric Current { get; }

        AggregatedMetric Aggregate(AggregatedMetric original, Metric metric);
    }
}