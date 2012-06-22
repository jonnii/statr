using Machine.Specifications;

namespace Statr.Specifications
{
    public class MetricSpecification
    {
        [Subject(typeof(Metric))]
        public class when_parsing_count
        {
            Because of = () =>
                metric = (CountMetric)Metric.Parse("metric.name:4|c");

            It should_have_name = () =>
                metric.Name.ShouldEqual("metric.name");

            It should_have_count = () =>
                metric.Amount.ShouldEqual(4);

            static CountMetric metric;
        }
    }
}
