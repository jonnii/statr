using Machine.Fakes;
using Machine.Specifications;

namespace Statr.Interactive.Specifications
{
    public class MetricsGeneratorSpecification
    {
        [Subject(typeof(MetricsGenerator))]
        public class when_parsing_simple : WithSubject<MetricsGenerator>
        {
            Because of = () =>
                request = Subject.BuildGeneratorRequest("s stats.fribble count 500 100 50");

            It should_parse_interval = () =>
            {
                request.Interval.From.ShouldEqual(100);
                request.Interval.To.ShouldEqual(100);
            };

            static GeneratorRequest request;
        }

        [Subject(typeof(MetricsGenerator))]
        public class when_parsing_with_ranges : WithSubject<MetricsGenerator>
        {
            Because of = () =>
                request = Subject.BuildGeneratorRequest("s stats.fribble count 500 100-200 50-150");

            It should_parse_interval = () =>
            {
                request.Interval.From.ShouldEqual(100);
                request.Interval.To.ShouldEqual(200);
            };

            It should_parse_value = () =>
            {
                request.Value.From.ShouldEqual(50);
                request.Value.To.ShouldEqual(150);
            };

            static GeneratorRequest request;
        }
    }
}
