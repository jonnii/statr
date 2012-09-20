using System.Collections.Generic;
using System.Linq;
using Machine.Fakes;
using Machine.Specifications;
using Statr.Server.Specifications.Fixtures;
using Statr.Server.Storage;

namespace Statr.Server.Specifications.Storage
{
    public class DataPointRepositorySpecification
    {
        [Subject(typeof(DataPointRepository))]
        public class when_getting_metrics_by_name : WithSubject<DataPointRepository>
        {
            Establish context = () =>
                The<IDataPointCache>().WhenToldTo(c => c.Get(Param.IsAny<BucketReference>())).Return(DataPointFixture.CreateMany(5));

            Because of = () =>
                points = Subject.Get(new BucketReference(MetricType.Count, "metric.name"));

            It should_get_data_points = () =>
                points.Count().ShouldEqual(5);

            It should_get_points_for_bucket = () =>
                The<IDataPointCache>().WasToldTo(c => c.Get(new BucketReference(MetricType.Count, "metric.name")));

            static IEnumerable<DataPoint> points;
        }
    }
}
