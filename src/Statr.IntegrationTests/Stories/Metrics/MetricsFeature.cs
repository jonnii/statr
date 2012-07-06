using System.Linq;
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
            When(TheMetric.IsRouted(Metric.Count("stats.application.metric", 500))).
            Then(TheMetricRouter.ShouldHaveRoutedNumMetrics(1));
        }

        [Test]
        public void ShouldRouteGaugeMetric()
        {
            Given(TheApplication.IsStarted).
            When(TheMetric.IsRouted(Metric.Gauge("stats.application.gauge", 500))).
            Then(TheMetricRouter.ShouldHaveRoutedNumMetrics(1));
        }

        [Test]
        public void ShouldGetMetricsAfterRouting()
        {
            Given(TheApplication.IsStarted).
            When(TheMetric.IsRouted(Metric.Count("stats.application.metric", 500))).
                And(TheMetrics.AreFlushed).
            Then(QueryFor.Metrics("stats.application.metric", MetricType.Count, m => m.Count() == 1));
        }
    }
}
