using Machine.Fakes;
using Machine.Specifications;
using Statr.Routing;

namespace Statr.Specifications.Routing
{
    public class MetricRouterSpecification
    {
        [Subject(typeof(MetricRouter))]
        public class when_routing : WithSubject<MetricRouter>
        {
            Establish context = () =>
            {
                route = An<IMetricRoute>();
                The<IMetricRouteRegistry>().WhenToldTo(r => r.GetRoutes(Param.IsAny<Metric>())).
                    Return(new[] { route });
            };

            Because of = () =>
                Subject.Route(new CountMetric("metric.name", 50));

            It should_notify_metric_routes = () =>
                 route.WasToldTo(r => r.Push(Param.IsAny<Metric>()));

            static IMetricRoute route;
        }
    }
}
