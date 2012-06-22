using System;
using NUnit.Framework;
using Statr.IntegrationTests.Fixtures;
using Statr.Storage;

namespace Statr.IntegrationTests.Manual
{
    [TestFixture]
    public class StorageTests
    {
        [Test, Explicit]
        public void ShouldSave()
        {
            var application = new IntegrationApplication();
            var container = application.Initialize();

            var storageEngineFactory = container.Resolve<IStorageEngineFactory>();
            var storageEngine = storageEngineFactory.Create(@"c:\dev\tmp\storage");

            var storageTree = storageEngine.CreateTree("tree");

            var node = storageTree.CreateNode("bucket", k =>
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
