using Machine.Fakes;
using Machine.Specifications;

namespace Statr.Specifications
{
    public class MetricParserSpecification
    {
        [Subject(typeof(MetricParser))]
        public class when_parsing_count : WithSubject<MetricParser>
        {
            Because of = () =>
                metric = Subject.Parse("metric.name:4|c");

            It should_have_name = () =>
                metric.Name.ShouldEqual("metric.name");

            It should_have_value = () =>
                metric.Value.ShouldEqual(4);

            It should_have_count_metric_type = () =>
                metric.MetricType.ShouldEqual(MetricType.Count);

            static Metric metric;
        }

        [Subject(typeof(MetricParser))]
        public class when_parsing_float_count : WithSubject<MetricParser>
        {
            Because of = () =>
                metric = Subject.Parse("metric.name:4.5|c");

            It should_have_name = () =>
                metric.Name.ShouldEqual("metric.name");

            It should_have_value = () =>
                metric.Value.ShouldEqual(4.5f);

            It should_have_count_metric_type = () =>
                metric.MetricType.ShouldEqual(MetricType.Count);

            static Metric metric;
        }

        [Subject(typeof(MetricParser))]
        public class when_parsing_gauge : WithSubject<MetricParser>
        {
            Because of = () =>
                metric = Subject.Parse("metric.name:666|g");

            It should_have_name = () =>
                metric.Name.ShouldEqual("metric.name");

            It should_have_value = () =>
                metric.Value.ShouldEqual(666);

            It should_have_gauge_metric_type = () =>
                metric.MetricType.ShouldEqual(MetricType.Gauge);

            static Metric metric;
        }
    }
}
