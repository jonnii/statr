using System;
using System.Collections.Generic;
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
                Subject.DataPoints.Subscribe(t => { dataPoint = t.DataPoint; });
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
            Establish context = () => Subject.DataPoints.Subscribe(t => raised = true);

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
                Subject.DataPoints.Subscribe(t => { dataPoint = t.DataPoint; });
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

        [Subject(typeof(MetricRouteManager))]
        public class with_long_running_route
        {
            Establish context = () =>
            {
                route = new MetricRoute(
                    new BucketReference("key", MetricType.Count),
                    120,
                    new AccumulateAggregationStrategy());

                route.Start();

                dataPoints = new List<DataPointEvent>();
                route.DataPoints.Subscribe(dataPoints.Add);

                route.Push(Metric.Count("key", 10));
                route.Push(Metric.Count("key", 10));
                route.Push(Metric.Count("key", 10));
            };

            Because of = () =>
                route.Dispose();

            It should_have_processed_metrics = () =>
                route.NumProcessedMetrics.ShouldEqual<ulong>(3);

            It should_have_published_data_points = () =>
                route.NumPublishedDataPoints.ShouldEqual<ulong>(1);

            It should_publish_data_point_with_values_aggregated_thus_far = () =>
                dataPoints.Single().DataPoint.Value.ShouldEqual(30);

            static MetricRoute route;

            static List<DataPointEvent> dataPoints;
        }

        public class with_route
        {
            Establish context = () =>
                Subject = new MetricRoute(new BucketReference("key", MetricType.Count), 1, new AccumulateAggregationStrategy());

            protected static MetricRoute Subject;
        }
    }
}
