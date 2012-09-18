using Machine.Fakes;
using Machine.Specifications;
using Statr.Server.Management.Controllers;
using Statr.Server.Storage;

namespace Statr.Server.Specifications.Management.Controllers
{
    public class DataPointsControllerSpecification
    {
        [Subject(typeof(DataPointsController))]
        public class on_get : WithSubject<DataPointsController>
        {
            Because of = () =>
                Subject.Get("bucket.id", "Count");

            It should_get_data_points_from_cache = () =>
                The<IDataPointCache>().WasToldTo(c => c.Get(Param.IsAny<BucketReference>()));
        }

        [Subject(typeof(DataPointsController))]
        public class when_parsing_metric_type : WithSubject<DataPointsController>
        {
            It should_parse_count = () =>
                Subject.ParseMetricType("Count").ShouldEqual(MetricType.Count);

            It should_parse_count_lower = () =>
                Subject.ParseMetricType("count").ShouldEqual(MetricType.Count);

            It should_parse_gauge = () =>
                Subject.ParseMetricType("Gauge").ShouldEqual(MetricType.Gauge);

            It should_parse_gauge_lower = () =>
                Subject.ParseMetricType("gauge").ShouldEqual(MetricType.Gauge);
        }
    }
}
