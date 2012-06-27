using System.Linq;
using System.Threading;
using Machine.Specifications;
using Statr.Routing;

namespace Statr.Specifications.Routing
{
    public class MetricRouteSpecification
    {
        [Subject(typeof(MetricRoute))]
        public class when_pushing : with_route
        {
            Establish context = () =>
            {
                Subject.DataPointGenerated += (o, e) => dataPoint = e.DataPoint;
                Subject.Start();
            };

            Because of = () =>
            {
                Subject.Push(new Metric("asdf", 5, MetricType.Count));
                Thread.Sleep(1200);
            };

            It should_create_data_point = () =>
                dataPoint.Value.ShouldEqual(5);

            It should_have_processed_metric = () =>
                Subject.NumProcessedMetrics.ShouldEqual<ulong>(1L);

            It should_have_published_data_point = () =>
                Subject.NumPublishedDataPoints.ShouldEqual<ulong>(1L);

            static DataPoint dataPoint;
        }

        [Subject(typeof(MetricRoute))]
        public class when_aggregating_multiple_metrics : with_route
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
                aggregated = metrics.Aggregate(new AggregatedMetric(), Subject.AggregateMetrics);

            It should_sum_metric_values = () =>
                aggregated.Value.ShouldEqual(10);

            static Metric[] metrics;

            static AggregatedMetric aggregated;
        }

        [Subject(typeof(MetricRoute))]
        public class when_raising_event_for_aggregated_metric_with_window_with_no_metrics : with_route
        {
            Establish context = () =>
                Subject.DataPointGenerated += (o, e) => raised = true;

            Because of = () =>
                Subject.OnMetricsAggregated(new AggregatedMetric { NumMetrics = 0 });

            It should_not_raise_event = () =>
                raised.ShouldBeFalse();

            static bool raised;
        }

        [Subject(typeof(MetricRoute))]
        public class when_pushing_gauge_metric : with_route
        {
            Establish context = () =>
            {
                Subject.DataPointGenerated += (o, e) => dataPoint = e.DataPoint;
                Subject.Start();
            };

            Because of = () =>
            {
                Subject.Push(new Metric("asdf", 5, MetricType.Gauge));
                Thread.Sleep(1200);
            };

            It should_create_data_point = () =>
                dataPoint.Value.ShouldEqual(5);

            It should_have_processed_metric = () =>
                Subject.NumProcessedMetrics.ShouldEqual<ulong>(1L);

            It should_have_published_data_point = () =>
                Subject.NumPublishedDataPoints.ShouldEqual<ulong>(1L);

            static DataPoint dataPoint;
        }

        public class with_route
        {
            Establish context = () =>
                Subject = new MetricRoute("key", 1);

            protected static MetricRoute Subject;
        }
    }
}
