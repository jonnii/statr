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
            Because of = () =>
                routes = Subject.GetRoutes(new CountMetric("stats.cputime", 50));

            It should_have_one_registered_route = () =>
                Subject.NumRoutes.ShouldEqual(1);

            It should_return_one_route = () =>
                routes.Count().ShouldEqual(1);

            static IEnumerable<IMetricRoute> routes;
        }

        [Subject(typeof(MetricRouteManager))]
        public class when_getting_route_for_existing_metric : with_configuration
        {
            Establish context = () =>
                Subject.GetRoutes(new CountMetric("stats.cputime", 50));

            Because of = () =>
                routes = Subject.GetRoutes(new CountMetric("stats.cputime", 50));

            It should_not_register_routes_twice = () =>
                Subject.NumRoutes.ShouldEqual(1);

            static IEnumerable<IMetricRoute> routes;
        }

        public class with_configuration : WithSubject<MetricRouteManager>
        {
            Establish context = () =>
            {
                var config = Config.Build(
                    c => c.AddEntry("stats", "^stats.", "1m:10d"));

                The<IConfigRepository>().WhenToldTo(c => c.GetConfiguration()).Return(config);
            };
        }
    }
}
