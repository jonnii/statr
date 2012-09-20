using System;
using Machine.Fakes;
using Machine.Specifications;
using Statr.Server.Publishing;

namespace Statr.Server.Specifications.Publishing
{
    public class TcpPublisherSpecification
    {
        [Subject(typeof(TcpPublisher))]
        public class when_serializing_data_point_event : WithSubject<TcpPublisher>
        {
            Because of = () =>
                serialized = Subject.Serialize(new DataPointEvent(new BucketReference(MetricType.Count, "bucket"),
                    new DataPoint(DateTime.Now, 50f, 10)));

            It should_serializer = () =>
                serialized.ShouldNotBeNull();

            static string serialized;
        }
    }
}
