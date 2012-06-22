using System;
using NUnit.Framework;
using Statr.IntegrationTests.Fixtures;
using Statr.Storage;

namespace Statr.IntegrationTests.Performance
{
    [TestFixture]
    public class SavingSliceData
    {
        [Test, Explicit]
        public void WritingSliceData()
        {
            var application = new IntegrationApplication();
            var container = application.Initialize();

            var storageEngineFactory = container.Resolve<IStorageEngineFactory>();
            var storageEngine = storageEngineFactory.Create(@"c:\dev\tmp\storage");

            var storageTree = storageEngine.CreateTree("performance");
            var node = storageTree.CreateNode("bucket", k =>
            {
                k.TimeStep = 1;
            });

            var slice = node.CreateSlice(DateTime.Now.Ticks, 1);

            var dataPoints = DataPoints.Create(1000000);
            var sliceData = dataPoints.ToSliceData();
            slice.Write(sliceData);
        }
    }
}
