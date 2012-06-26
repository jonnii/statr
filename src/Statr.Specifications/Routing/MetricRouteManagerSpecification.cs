using System.Collections.Generic;
using System.Linq;
using Machine.Fakes;
using Machine.Specifications;
using Statr.Configuration;
using Statr.Routing;

namespace Statr.Specifications.Routing
{
    public class MetricRouteManagerSpecification
    {
        [Subject(typeof(MetricRouteManager))]
        public class when_getting_routes : with_configuration
        {
            Establish context = () =>
            {
                route = An<IMetricRoute>();

                metricRouteFactory.WhenToldTo(r => r.Build(Param.IsAny<string>(), Param.IsAny<Retention>()))
                    .Return(route);
            };

            Because of = () =>
                routes = Subject.GetRoutes(new CountMetric("stats.cputime", 50));

            It should_have_one_registered_route = () =>
                Subject.NumRoutes.ShouldEqual(1);

            It should_return_one_route = () =>
                routes.Count().ShouldEqual(1);

            It should_start_route = () =>
                route.WasToldTo(r => r.Start());

            static IEnumerable<IMetricRoute> routes;

            static IMetricRoute route;
        }

        [Subject(typeof(MetricRouteManager))]
        public class when_getting_route_for_existing_metric : with_configuration
        {
            Establish context = () =>
            {
                metricRouteFactory.WhenToldTo(r => r.Build(Param.IsAny<string>(), Param.IsAny<Retention>()))
                    .Return(An<IMetricRoute>());

                Subject.GetRoutes(new CountMetric("stats.cputime", 50));
            };

            Because of = () =>
                Subject.GetRoutes(new CountMetric("stats.cputime", 50));

            It should_not_register_routes_twice = () =>
                Subject.NumRoutes.ShouldEqual(1);
        }

        [Subject(typeof(MetricRouteManager))]
        public class with_built_route : with_metric_route_manager
        {
            Establish context = () =>
            {
                route = new Moq.Mock<IMetricRoute>();

                metricRouteFactory.WhenToldTo(f => f.Build(Param.IsAny<string>(), Param.IsAny<Retention>()))
                    .Return(route.Object);

                Subject.BuildRoute("metric.name", new Retention(1, 60));
            };

            Because of = () =>
                route.Raise(r => r.DataPointGenerated += null, new DataPointEventArgs(new DataPoint(1, 1)));

            It should_notify_all_data_point_subscribers = () =>
                dataPointSubscriber.WasToldTo(r => r.Push(Param.IsAny<DataPoint>()));

            static Moq.Mock<IMetricRoute> route;
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
                    c => c.AddEntry("stats", "^stats.", "1m:10d"));

                configRepository.WhenToldTo(c => c.GetConfiguration()).Return(config);
            };
        }
    }
}
