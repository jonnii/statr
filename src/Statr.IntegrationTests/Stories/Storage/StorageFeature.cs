using System.Linq;
using NUnit.Framework;
using Statr.IntegrationTests.Stories.Steps;
using Statr.Server;

namespace Statr.IntegrationTests.Stories.Storage
{
    [TestFixture]
    public class StorageFeature : StatrStory
    {
        [Test]
        public void SavingDataPoints()
        {
            Given(TheApplication.IsStarted).
            When(TheStorageEngine.WritesDataPoints("stats.example", MetricType.Count)).
            Then(TheStorageEngine.ShouldHaveCreatedDirectory("default/Count/stats.example"));
        }

        [Test]
        public void ReadingBuckets()
        {
            Given(TheApplication.IsStarted).
            And(TheStorageEngine.WritesDataPoints("stats.example", MetricType.Count)).
            Then(TheStorageEngine.ShouldReadBuckets(
                buckets => buckets.Any(b => b.Name == "stats.example" && b.MetricType == MetricType.Count)));
        }
    }
}
