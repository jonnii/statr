using Machine.Specifications;
using Statr.Routing;

namespace Statr.Specifications.Routing
{
    public class BucketSpecification
    {
        [Subject(typeof(Bucket))]
        public class with_count_bucket
        {
            Establish context = () =>
                bucket = new Bucket("metric.name", MetricType.Count);

            Because of = () =>
                strategy = bucket.BuildAggregationStrategy();

            It should_create_accumulate_strategy = () =>
                strategy.ShouldBeOfType<AccumulateAggregationStrategy>();

            static Bucket bucket;

            static IAggregationStrategy strategy;
        }

        [Subject(typeof(Bucket))]
        public class with_gauge_bucket
        {
            Establish context = () =>
                bucket = new Bucket("metric.name", MetricType.Gauge);

            Because of = () =>
                strategy = bucket.BuildAggregationStrategy();

            It should_create_last_value_bucket = () =>
                strategy.ShouldBeOfType<LastValueAggregationStrategy>();

            static Bucket bucket;

            static IAggregationStrategy strategy;
        }
    }
}
