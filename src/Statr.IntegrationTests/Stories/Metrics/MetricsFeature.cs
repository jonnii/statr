using NUnit.Framework;
using Statr.IntegrationTests.Stories.Steps;

namespace Statr.IntegrationTests.Stories.Metrics
{
    [TestFixture]
    public class MetricsFeature : StatrStory
    {
        [Test]
        public void ShouldRouteCountMetric()
        {
            Given(TheApplication.IsStarted).
            When(TheMetric.IsRouted(new Metric("stats.application.metric", 500, MetricType.Count))).
            Then(TheMetricRouter.ShouldHaveRoutedNumMetrics(1));
        }

        [Test]
        public void ShouldRouteGaugeMetric()
        {
            Given(TheApplication.IsStarted).
            When(TheMetric.IsRouted(new Metric("stats.application.gauge", 500, MetricType.Gauge))).
            Then(TheMetricRouter.ShouldHaveRoutedNumMetrics(1));
        }
    }
}
