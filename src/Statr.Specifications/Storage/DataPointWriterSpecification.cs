using System;
using System.Reactive.Linq;
using Machine.Fakes;
using Machine.Specifications;
using Statr.Routing;
using Statr.Storage;
using Statr.Storage.Strategies;

namespace Statr.Specifications.Storage
{
    public class DataPointWriterSpecification
    {
        [Subject(typeof(DataPointWriter))]
        public class in_general : WithSubject<DataPointWriter>
        {
            It should_have_default_tree_name = () =>
                Subject.StorageTreeName.ShouldEqual("default");
        }

        [Subject(typeof(DataPointWriter))]
        public class when_starting : with_storage_engine
        {
            Establish context = () =>
                The<IDataPointStream>().WhenToldTo(s => s.DataPoints)
                    .Return(new[]
                    {
                        new DataPointEvent(
                            new BucketReference("bucket", MetricType.Count), 
                            new DataPoint(DateTime.Now, 500, 1))
                    }.ToObservable());

            Because of = () =>
                Subject.Start();

            It should_get_or_create_node_for_bucket = () =>
                storageTree.WasToldTo(s => s.GetOrCreateNode("bucket"));

            It should_save_points_in_storage_node = () =>
                storageNode.WasToldTo(s => s.Store(Param.IsAny<DataPointCollection>()));
        }

        public class with_storage_engine : WithSubject<DataPointWriter>
        {
            Establish context = () =>
            {
                The<IStorageStrategyFactory>().WhenToldTo(r => r.Build()).Return(new ImmediateStorageStrategy());

                storageTree = An<IStorageTree>();
                storageNode = An<IStorageNode>();

                The<IStorageEngine>().WhenToldTo(e => e.GetOrCreateTree(Param.IsAny<string>()))
                    .Return(storageTree);

                storageTree.WhenToldTo(t => t.GetOrCreateNode(Param.IsAny<string>())).Return(storageNode);
            };

            protected static IStorageTree storageTree;

            protected static IStorageNode storageNode;
        }
    }
}
