using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using Machine.Fakes;
using Machine.Specifications;
using Statr.Routing;
using Statr.Storage;
using Statr.Storage.Strategies;

namespace Statr.Specifications.Storage
{
    public class DataPointBufferSpecification
    {
        [Subject(typeof(DataPointBuffer))]
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

            It should_save_points_in_storage_node = () =>
                writer.WasToldTo(s => s.Write(Param.IsAny<IEnumerable<DataPoint>>()));
        }

        public class with_storage_engine : WithSubject<DataPointBuffer>
        {
            Establish context = () =>
            {
                The<IBufferStrategyFactory>().WhenToldTo(
                    r => r.Build(Param.IsAny<BucketReference>())).Return(new ImmediateStrategy());

                writer = An<IDataPointWriter>();

                The<IStorageEngine>().WhenToldTo(e => e.GetWriter(new BucketReference("bucket", MetricType.Count)))
                    .Return(writer);
            };

            protected static IDataPointWriter writer;
        }
    }
}
