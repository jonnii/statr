using NUnit.Framework;
using Statr.IntegrationTests.Stories.Steps;
using Statr.Server;

namespace Statr.IntegrationTests.Stories.Publishing
{
    [TestFixture]
    public class PublisherFeature : StatrStory
    {
        [Test]
        public void ShouldGetMetricsAfterRouting()
        {
            Given(TheApplication.IsStarted).
                And(TheDataPointSubscriber.IsListening()).
            When(TheMetric.IsRouted(Metric.Count("stats.application.metric", 500))).
                And(TheMetrics.AreFlushed).
            Then(TheDataPointSubscriber.ShouldHaveReceivedMetrics(1));
        }
    }
}
