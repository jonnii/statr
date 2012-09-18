using System;
using Machine.Fakes;
using Machine.Specifications;
using Statr.Server.Storage.Engine;

namespace Statr.Server.Specifications.Storage.Engine
{
    public class StorageEngineDataPointWriterSpecification
    {
        [Subject(typeof(StorageEngineDataPointWriter))]
        public class when_writing_data_points : with_writer
        {
            Because of = () =>
                Subject.Write(new[] { new DataPoint(DateTime.Now, 30f, 1) });

            It should_get_node_for_bucket = () =>
                tree.WasToldTo(t => t.GetOrCreateNode("bucket.name"));
        }

        public class with_writer : WithFakes
        {
            Establish context = () =>
            {
                node = An<IStorageNode>();

                tree = An<IStorageTree>();
                tree.WhenToldTo(t => t.GetOrCreateNode(Param.IsAny<string>())).Return(node);

                Subject = new StorageEngineDataPointWriter(tree, new BucketReference("bucket.name", MetricType.Count));
            };

            protected static StorageEngineDataPointWriter Subject;

            protected static IStorageTree tree;

            protected static IStorageNode node;
        }
    }
}
