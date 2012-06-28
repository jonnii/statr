using Machine.Fakes;
using Machine.Specifications;

namespace Statr.Interactive.Specifications
{
    public class MetricsGeneratorSpecification
    {
        [Subject(typeof(MetricsGenerator))]
        public class when : WithSubject<MetricsGenerator>
        {
            Because of = () =>
                request = Subject.BuildGeneratorRequest("s stats.fribble count 500 100 50");

            It should_parse_interval = () =>
                request.Interval.ShouldEqual(100);

            static GeneratorRequest request;
        }
    }
}
