using System;
using NUnit.Framework;
using Statr.IntegrationTests.Fixtures;
using Statr.Storage;

namespace Statr.IntegrationTests.Manual
{
    [TestFixture]
    public class StorageTests : ContainerTest
    {
        [Test, Explicit]
        public void ShouldSave()
        {
            var container = GetContainer();

            var storageEngine = container.Resolve<IStorageEngine>();

            var storageTree = storageEngine.GetOrCreateTree("tree");

            var node = storageTree.GetOrCreateNode("bucket", k =>
            {
                k.TimeStep = 1;
            });

            var dataPoints = DataPoints.Create();

            var sliceData = dataPoints.ToSliceData();

            var slice = node.CreateSlice(DateTime.Now.Ticks, 1);
            slice.Write(sliceData);

            var data = slice.Read();
            foreach (var item in data)
            {
                Console.WriteLine(item);
            }
        }
    }
}
