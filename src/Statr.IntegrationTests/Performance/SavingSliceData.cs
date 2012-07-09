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
            application.Initialize();
            var container = application.Container;

            var storageEngine = container.Resolve<IStorageEngine>();

            var storageTree = storageEngine.GetOrCreateTree("performance");
            var node = storageTree.GetOrCreateNode("bucket", k =>
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
