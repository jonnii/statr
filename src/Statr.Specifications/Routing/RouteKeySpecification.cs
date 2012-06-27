using Machine.Specifications;
using Statr.Routing;

namespace Statr.Specifications.Routing
{
    public class RouteKeySpecification
    {
        [Subject(typeof(RouteKey))]
        public class with_count_route_key
        {
            Establish context = () =>
                key = new RouteKey("metric.name", MetricType.Count);

            Because of = () =>
                strategy = key.BuildAggregationStrategy();

            It should_create_accumulate_strategy = () =>
                strategy.ShouldBeOfType<AccumulateAggregationStrategy>();

            static RouteKey key;

            static IAggregationStrategy strategy;
        }

        [Subject(typeof(RouteKey))]
        public class with_gauge_route_key
        {
            Establish context = () =>
                key = new RouteKey("metric.name", MetricType.Gauge);

            Because of = () =>
                strategy = key.BuildAggregationStrategy();

            It should_create_last_value_route_key = () =>
                strategy.ShouldBeOfType<LastValueAggregationStrategy>();

            static RouteKey key;

            static IAggregationStrategy strategy;
        }
    }
}
