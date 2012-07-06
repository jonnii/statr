using System.Collections.Generic;
using System.Linq;
using Machine.Fakes;
using Machine.Specifications;
using Statr.Routing;
using Statr.Specifications.Fixtures;
using Statr.Storage;

namespace Statr.Specifications.Storage
{
    public class DataPointRepositorySpecification
    {
        [Subject(typeof(DataPointRepository))]
        public class when_getting_metrics_by_name : WithSubject<DataPointRepository>
        {
            Establish context = () =>
                The<IDataPointCache>().WhenToldTo(c => c.Get(Param.IsAny<RouteKey>())).Return(DataPointFixture.CreateMany(5));

            Because of = () =>
                points = Subject.Get(new RouteKey("metric.name", MetricType.Count));

            It should_get_data_points = () =>
                points.Count().ShouldEqual(5);

            It should_get_points_for_route_key = () =>
                The<IDataPointCache>().WasToldTo(c => c.Get(new RouteKey("metric.name", MetricType.Count)));

            static IEnumerable<DataPoint> points;
        }
    }
}
