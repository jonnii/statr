using Machine.Fakes;
using Machine.Specifications;
using Statr.Configuration;
using Statr.Routing;

namespace Statr.Specifications.Routing
{
    public class MetricRouteManagerSpecification
    {
        [Subject(typeof(MetricRouteManager))]
        public class when_getting_route : with_configuration
        {
            Establish context = () =>
            {
                route = An<IMetricRoute>();

                metricRouteFactory.WhenToldTo(r => r.Build(Param.IsAny<string>(), Param.IsAny<int>()))
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
                metricRouteFactory.WhenToldTo(r => r.Build(Param.IsAny<string>(), Param.IsAny<int>()))
                    .Return(An<IMetricRoute>());

                Subject.GetRoute(new Metric("stats.cputime", 50, MetricType.Count));
            };

            Because of = () =>
                Subject.GetRoute(new Metric("stats.cputime", 50, MetricType.Count));

            It should_not_register_routes_twice = () =>
                Subject.NumRoutes.ShouldEqual(1);
        }

        [Subject(typeof(MetricRouteManager))]
        public class with_built_route : with_metric_route_manager
        {
            Establish context = () =>
            {
                route = new Moq.Mock<IMetricRoute>();

                metricRouteFactory.WhenToldTo(f => f.Build(Param.IsAny<string>(), Param.IsAny<int>()))
                    .Return(route.Object);

                Subject.BuildRoute("metric.name", new Retention(1, 60));
            };

            Because of = () =>
                route.Raise(r => r.DataPointGenerated += null, new DataPointEventArgs(new DataPoint(1, 1)));

            It should_notify_all_data_point_subscribers = () =>
                dataPointSubscriber.WasToldTo(r => r.Push(Param.IsAny<DataPoint>()));

            static Moq.Mock<IMetricRoute> route;
        }

        [Subject(typeof(MetricRouteManager))]
        public class when_building_route : with_configuration
        {
            Establish context = () =>
                metricRouteFactory.WhenToldTo(f => f.Build("stats.awesome", 60))
                    .Return(An<IMetricRoute>());

            Because of = () =>
                route = Subject.BuildRoute("stats.awesome");

            It should_build_metric_route_for_highest_frequency_retention_period = () =>
                route.ShouldNotBeNull();

            static IMetricRoute route;
        }

        public class with_metric_route_manager : WithFakes
        {
            Establish context = () =>
            {
                metricRouteFactory = An<IMetricRouteFactory>();
                configRepository = An<IConfigRepository>();
                dataPointSubscriber = An<IDataPointSubscriber>();

                Subject = new MetricRouteManager(
                    metricRouteFactory,
                    configRepository,
                    dataPointSubscriber);
            };

            protected static MetricRouteManager Subject;

            protected static IMetricRouteFactory metricRouteFactory;

            protected static IConfigRepository configRepository;

            protected static IDataPointSubscriber dataPointSubscriber;
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
