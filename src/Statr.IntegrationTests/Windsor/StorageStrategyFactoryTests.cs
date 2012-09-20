using NUnit.Framework;
using Statr.Server;
using Statr.Server.Storage;
using Statr.Server.Storage.Strategies;

namespace Statr.IntegrationTests.Windsor
{
    [TestFixture]
    public class StorageStrategyFactoryTests : ContainerTest
    {
        [Test]
        public void ShouldBuildDefault()
        {
            using (var container = GetContainer())
            {
                var storageStrategyFactory = container.Resolve<IBufferStrategyFactory>();
                var strategy = storageStrategyFactory.Build(new BucketReference(MetricType.Count, "unknown"));

                Assert.That(strategy, Is.TypeOf<BufferedStrategy>());
            }
        }

        [Test]
        public void ShouldBuild()
        {
            using (var container = GetContainer())
            {
                var storageStrategyFactory = container.Resolve<IBufferStrategyFactory>();
                var strategy = storageStrategyFactory.Build(new BucketReference(MetricType.Count, "immedate.bucket"));

                Assert.That(strategy, Is.TypeOf<ImmediateStrategy>());
            }
        }

        [Test]
        public void ShouldBuildWithProperties()
        {
            using (var container = GetContainer())
            {
                var storageStrategyFactory = container.Resolve<IBufferStrategyFactory>();
                var strategy = (BufferedStrategy)storageStrategyFactory.Build(new BucketReference(MetricType.Count, "buffered.with.properties"));

                Assert.That(strategy.BufferSize, Is.EqualTo(10000));
            }
        }
    }
}
