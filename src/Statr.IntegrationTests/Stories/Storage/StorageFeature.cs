using System;
using NUnit.Framework;
using Statr.IntegrationTests.Stories.Steps;
using Statr.Server;

namespace Statr.IntegrationTests.Stories.Storage
{
    [TestFixture]
    public class StorageFeature : StatrStory
    {
        [Test]
        public void CreatingTree()
        {
            Given(TheApplication.IsStarted).
            When(TheStorageEngine.WritesDataPoints("stats.example", MetricType.Count, new DataPoint(DateTime.Now, 300f, 0))).
            Then(TheStorageEngine.ShouldHaveCreatedDirectory("default/Count/stats.example"));
        }
    }
}
