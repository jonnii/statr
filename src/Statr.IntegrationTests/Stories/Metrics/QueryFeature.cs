using System.Linq;
using NUnit.Framework;
using Statr.IntegrationTests.Fixtures;
using Statr.IntegrationTests.Stories.Steps;
using Statr.Server;

namespace Statr.IntegrationTests.Stories.Metrics
{
    [TestFixture]
    public class QueryFeature : StatrStory
    {
        [Test]
        public void QueryingForCachedDataPoints()
        {
            Given(TheApplication.IsStarted).
                And(TheMetric.IsRouted(Metric.Count("stats.historical", 500))).
                And(TheMetrics.AreFlushed).
            When(TheQueryEngine.Fetches("stats.historical", MetricType.Count)).
            Then(TheLastQuery.ShouldHave(q => q.Count() == 1));
        }

        [Test, Ignore]
        public void QueryingForPersistedDataPoints()
        {
            Given(TheApplication.IsStarted).
                And(TheStorageEngine.WritesDataPoints("stats.historical", MetricType.Count, DataPoints.Create())).
            When(TheQueryEngine.Fetches("stats.historical", MetricType.Count)).
            Then(TheLastQuery.ShouldHave(q => q.Count() == 100));
        }
    }
}
