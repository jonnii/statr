using System.Linq;
using Machine.Fakes;
using Machine.Specifications;
using Statr.Routing;

namespace Statr.Specifications.Routing
{
    public class AccumulateAggregationStrategySpecification
    {
        [Subject(typeof(AccumulateAggregationStrategy))]
        public class when_aggregating_multiple_metrics : WithSubject<AccumulateAggregationStrategy>
        {
            Establish context = () =>
            {
                metrics = new[]
                {
                    new Metric("key", 5f, MetricType.Count),
                    new Metric("key", 5f, MetricType.Count)
                };
            };

            Because of = () =>
                aggregated = metrics.Aggregate(new AggregatedMetric(), Subject.Aggregate);

            It should_sum_metric_values = () =>
                aggregated.Value.ShouldEqual(10);

            static Metric[] metrics;

            static AggregatedMetric aggregated;
        }
    }
}
