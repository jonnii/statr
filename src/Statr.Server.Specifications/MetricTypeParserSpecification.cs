using Machine.Specifications;

namespace Statr.Server.Specifications
{
    public class MetricTypeParserSpecification
    {
        [Subject(typeof(MetricTypeParser))]
        public class when_parsing_metric_type
        {
            It should_parse_count = () =>
                MetricTypeParser.Parse("Count").ShouldEqual(MetricType.Count);

            It should_parse_count_lower = () =>
                MetricTypeParser.Parse("count").ShouldEqual(MetricType.Count);

            It should_parse_gauge = () =>
                MetricTypeParser.Parse("Gauge").ShouldEqual(MetricType.Gauge);

            It should_parse_gauge_lower = () =>
                MetricTypeParser.Parse("gauge").ShouldEqual(MetricType.Gauge);
        }
    }
}
