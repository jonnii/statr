using System.Linq;
using Machine.Fakes;
using Machine.Specifications;
using Statr.Routing;

namespace Statr.Specifications.Routing
{
    public class LastValueAggregationStrategySpecification
    {
        [Subject(typeof(LastValueAggregationStrategy))]
        public class when_aggregating_multiple_metrics : WithSubject<LastValueAggregationStrategy>
        {
            Establish context = () =>
            {
                metrics = new[]
                {
                    new Metric("key", 5f, MetricType.Count),
                    new Metric("key", 15f, MetricType.Count)
                };
            };

            Because of = () =>
                aggregated = metrics.Aggregate(new AggregatedMetric(), Subject.Aggregate);

            It should_use_value_of_last_metric = () =>
                aggregated.Value.ShouldEqual(15);

            static Metric[] metrics;

            static AggregatedMetric aggregated;
        }
    }
}
