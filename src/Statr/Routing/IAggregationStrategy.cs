namespace Statr.Routing
{
    public interface IAggregationStrategy
    {
        AggregatedMetric Aggregate(AggregatedMetric original, Metric metric);
    }
}