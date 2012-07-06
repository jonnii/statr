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
                Subject = new MetricRoute(new BucketReference("key", MetricType.Count), 1, new AccumulateAggregationStrategy());

            protected static MetricRoute Subject;
        }
    }
}
