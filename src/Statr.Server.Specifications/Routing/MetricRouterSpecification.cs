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
                The<IMetricRouteManager>().WhenToldTo(r => r.GetRoute(Param.IsAny<Metric>())).Return(route);
            };

            Because of = () =>
                Subject.Route(new Metric("metric.name", 50, MetricType.Count));

            It should_notify_metric_routes = () =>
                 route.WasToldTo(r => r.Push(Param.IsAny<Metric>()));

            It should_have_processed_metric = () =>
                Subject.NumProcessedMetrics.ShouldEqual<ulong>(1);

            static IMetricRoute route;
        }
    }
}
