namespace Statr.Routing
{
    public interface IMetricRouter
    {
        ulong NumProcessedMetrics { get; }

        void Route(Metric metric);
    }
}