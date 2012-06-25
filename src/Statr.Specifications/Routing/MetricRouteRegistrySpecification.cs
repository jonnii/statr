using System.Collections.Generic;
using System.Linq;
using Machine.Fakes;
using Machine.Specifications;
using Statr.Config;
using Statr.Routing;

namespace Statr.Specifications.Routing
{
    public class MetricRouteRegistrySpecification
    {
        [Subject(typeof(MetricRouteRegistry))]
        public class when_getting_routes : WithSubject<MetricRouteRegistry>
        {
            Establish context = () =>
                Subject.RegisterRoute(new RouteDefinition(new StorageEntry("all stats", "^stats", new Retention("10s", "10d"))));

            Because of = () =>
                routes = Subject.GetRoutes(new CountMetric("stats.cputime", 50));

            It should_have_one_registerd_route = () =>
                Subject.NumRoutes.ShouldEqual(1);

            It should_return_one_route = () =>
                routes.Count().ShouldEqual(1);

            static IEnumerable<IMetricRoute> routes;
        }

        [Subject(typeof(MetricRouteRegistry))]
        public class when_getting_route_for_existing_metric : WithSubject<MetricRouteRegistry>
        {
            Establish context = () =>
            {
                Subject.RegisterRoute(new RouteDefinition(new StorageEntry("all stats", "^stats", new Retention("10s", "10d"))));
                Subject.GetRoutes(new CountMetric("stats.cputime", 50));
            };

            Because of = () =>
                routes = Subject.GetRoutes(new CountMetric("stats.cputime", 50));

            It should_not_register_routes_twice = () =>
                Subject.NumRoutes.ShouldEqual(1);

            static IEnumerable<IMetricRoute> routes;
        }
    }
}
