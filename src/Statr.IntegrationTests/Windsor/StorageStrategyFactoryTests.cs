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
            var container = GetContainer();

            var storageStrategyFactory = container.Resolve<IBufferStrategyFactory>();
            var strategy = storageStrategyFactory.Build(new BucketReference("unknown", MetricType.Count));

            Assert.That(strategy, Is.TypeOf<BufferedStrategy>());
        }

        [Test]
        public void ShouldBuild()
        {
            var container = GetContainer();

            var storageStrategyFactory = container.Resolve<IBufferStrategyFactory>();
            var strategy = storageStrategyFactory.Build(new BucketReference("immedate.bucket", MetricType.Count));

            Assert.That(strategy, Is.TypeOf<ImmediateStrategy>());
        }

        [Test]
        public void ShouldBuildWithProperties()
        {
            var container = GetContainer();

            var storageStrategyFactory = container.Resolve<IBufferStrategyFactory>();
            var strategy = (BufferedStrategy)storageStrategyFactory.Build(new BucketReference("buffered.with.properties", MetricType.Count));

            Assert.That(strategy.BufferSize, Is.EqualTo(10000));
        }
    }
}
