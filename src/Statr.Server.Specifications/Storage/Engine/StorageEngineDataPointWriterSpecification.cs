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

            It should_get_or_create_tree_for_namespace_and_metric_type = () =>
                root.WasToldTo(r => r.GetOrCreateTree("default/Count"));

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

                root = An<IStorageTreeRoot>();
                root.WhenToldTo(r => r.GetOrCreateTree(Param.IsAny<string>())).Return(tree);


                Subject = new StorageEngineDataPointWriter(root, new BucketReference("bucket.name", MetricType.Count));
            };

            protected static StorageEngineDataPointWriter Subject;

            protected static IStorageTreeRoot root;

            protected static IStorageTree tree;

            protected static IStorageNode node;
        }
    }
}
