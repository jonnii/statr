﻿using System.Collections.Generic;
using Machine.Fakes;
using Machine.Specifications;
using Statr.Server.Configuration;
using Statr.Server.Routing;
using Statr.Server.Storage;

namespace Statr.Server.Specifications.Routing
{
    public class MetricRouteManagerSpecification
    {
        [Subject(typeof(MetricRouteManager))]
        public class when_getting_route : with_configuration
        {
            Establish context = () =>
            {
                route = An<IMetricRoute>();

                metricRouteFactory.WhenToldTo(r => r.Build(Param.IsAny<BucketReference>(), Param.IsAny<int>(), Param.IsAny<IAggregationStrategy>()))
                    .Return(route);
            };

            Because of = () =>
                Subject.GetRoute(new Metric("stats.cputime", 50, MetricType.Count));

            It should_have_one_registered_route = () =>
                Subject.NumRoutes.ShouldEqual(1);

            It should_start_route = () =>
                route.WasToldTo(r => r.Start());

            static IMetricRoute route;
        }

        [Subject(typeof(MetricRouteManager))]
        public class when_getting_route_for_existing_metric : with_configuration
        {
            Establish context = () =>
            {
                metricRouteFactory.WhenToldTo(r => r.Build(Param.IsAny<BucketReference>(), Param.IsAny<int>(), Param.IsAny<IAggregationStrategy>()))
                    .Return(An<IMetricRoute>());

                Subject.GetRoute(new Metric("stats.cputime", 50, MetricType.Count));
            };

            Because of = () =>
                Subject.GetRoute(new Metric("stats.cputime", 50, MetricType.Count));

            It should_not_register_routes_twice = () =>
                Subject.NumRoutes.ShouldEqual(1);
        }

        [Subject(typeof(MetricRouteManager))]
        public class when_getting_route_for_existing_metric_name_and_different_metric_type : with_configuration
        {
            Establish context = () =>
            {
                metricRouteFactory.WhenToldTo(r => r.Build(Param.IsAny<BucketReference>(), Param.IsAny<int>(), Param.IsAny<IAggregationStrategy>()))
                    .Return(() => An<IMetricRoute>());

                countRoute = Subject.GetRoute(new Metric("stats.cputime", 50, MetricType.Count));
            };

            Because of = () =>
                gaugeRoute = Subject.GetRoute(new Metric("stats.cputime", 50, MetricType.Gauge));

            It should_not_register_routes_twice = () =>
                Subject.NumRoutes.ShouldEqual(2);

            It should_create_different_route_for_different_metric_types = () =>
                gaugeRoute.ShouldNotBeTheSameAs(countRoute);

            static IEnumerable<IMetricRoute> countRoute;

            static IEnumerable<IMetricRoute> gaugeRoute;
        }

        [Subject(typeof(MetricRouteManager))]
        public class with_built_route : with_metric_route_manager
        {
            Establish context = () =>
            {
                route = new Moq.Mock<IMetricRoute>();
                bucket = new BucketReference(MetricType.Count, "stats.awesome");

                metricRouteFactory.WhenToldTo(f => f.Build(Param.IsAny<BucketReference>(), Param.IsAny<int>(), Param.IsAny<IAggregationStrategy>()))
                    .Return(route.Object);
            };

            Because of = () =>
               Subject.BuildRoute(bucket, new Retention(1, 60));

            It should_register_route_with_data_point_generator = () =>
                dataPointStream.WasToldTo(s => s.Register(Param.IsAny<IDataPointGenerator>()));

            static Moq.Mock<IMetricRoute> route;

            static BucketReference bucket;
        }

        [Subject(typeof(MetricRouteManager))]
        public class when_flushing_routes : with_configuration
        {
            Establish context = () =>
            {
                metricRouteFactory.WhenToldTo(f => f.Build(Param.IsAny<BucketReference>(), Param.IsAny<int>(), Param.IsAny<IAggregationStrategy>()))
                    .Return(An<IMetricRoute>());

                Subject.GetRoute(Metric.Count("stats.awesome", 60));
            };

            Because of = () =>
                Subject.FlushAll();

            It should_release_route = () =>
                metricRouteFactory.WasToldTo(f => f.Release(Param.IsAny<IMetricRoute>()));

            It should_clear_out_routes = () =>
                Subject.NumRoutes.ShouldEqual(0);
        }

        public class with_metric_route_manager : WithFakes
        {
            Establish context = () =>
            {
                metricRouteFactory = An<IMetricRouteFactory>();
                configRepository = An<IConfigRepository>();
                dataPointStream = An<IDataPointStream>();
                bucketRepository = An<IBucketRepository>();

                bucketRepository.WhenToldTo(r => r.Get(Param.IsAny<BucketReference>())).Return(new Bucket("bucket", MetricType.Count));

                Subject = new MetricRouteManager(
                    metricRouteFactory,
                    bucketRepository,
                    configRepository,
                    dataPointStream);
            };

            protected static MetricRouteManager Subject;

            protected static IMetricRouteFactory metricRouteFactory;

            protected static IConfigRepository configRepository;

            protected static IDataPointStream dataPointStream;

            private static IBucketRepository bucketRepository;
        }

        public class with_configuration : with_metric_route_manager
        {
            Establish context = () =>
            {
                var config = Config.Build(
                    c => c.AddEntry("stats", "^stats.", "1m:10d", "5m:30d"));

                configRepository.WhenToldTo(c => c.GetConfiguration()).Return(config);
            };
        }
    }
}
