using System.Linq;
using NUnit.Framework;
using Statr.Server;
using Statr.Server.Storage;
using Statr.Server.Storage.Engine;

namespace Statr.IntegrationTests.Components
{
    [TestFixture]
    public class StorageEngineTests : ContainerTest
    {
        [Test]
        public void ShouldCreateSliceWithPoints()
        {
            using (var container = GetContainer())
            {
                var storageEngine = (StorageEngine)container.Resolve<IStorageEngine>();

                var bucket = new BucketReference("stats.component.tests", MetricType.Count);
                const long when = 634836685639422723; // 19th sept 2012

                var tree = storageEngine.GetStorageTree(bucket);
                tree.DeleteAllNodes();

                var writer = storageEngine.GetWriter(bucket);
                writer.Write(new[] { new DataPoint(when, 50f, 1) });

                var node = tree.GetOrCreateNode("stats.component.tests");

                var slices = node.GetSlices();
                Assert.That(slices.Count(), Is.EqualTo(1));

                var slice1 = (StorageSlice)slices.First();

                Assert.That(slice1.StartTime, Is.EqualTo(when));

                var points = node.Read();
                Assert.That(points.Count(), Is.EqualTo(1));
            }
        }
    }
}
