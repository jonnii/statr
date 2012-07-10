using NUnit.Framework;
using Statr.Storage;
using Statr.Storage.Strategies;

namespace Statr.IntegrationTests.Windsor
{
    [TestFixture]
    public class StorageStrategyFactoryTests : ContainerTest
    {
        [Test]
        public void ShouldBuildDefault()
        {
            var container = GetContainer();

            var storageStrategyFactory = container.Resolve<IStorageStrategyFactory>();
            var strategy = storageStrategyFactory.Build(new BucketReference("unknown", MetricType.Count));

            Assert.That(strategy, Is.TypeOf<BufferedStorageStrategy>());
        }

        [Test]
        public void ShouldBuild()
        {
            var container = GetContainer();

            var storageStrategyFactory = container.Resolve<IStorageStrategyFactory>();
            var strategy = storageStrategyFactory.Build(new BucketReference("immedate.bucket", MetricType.Count));

            Assert.That(strategy, Is.TypeOf<ImmediateStorageStrategy>());
        }

        [Test]
        public void ShouldBuildWithProperties()
        {
            var container = GetContainer();

            var storageStrategyFactory = container.Resolve<IStorageStrategyFactory>();
            var strategy = (BufferedStorageStrategy)storageStrategyFactory.Build(new BucketReference("buffered.with.properties", MetricType.Count));

            Assert.That(strategy.BufferSize, Is.EqualTo(10000));
        }
    }
}
