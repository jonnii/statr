using System;
using Statr.Routing;

namespace Statr.IntegrationTests.Stories.Steps
{
    public static class TheMetric
    {
        public static Action<StatrContext> IsRouted(Metric metric)
        {
            return context =>
            {
                var router = context.Container.Resolve<IMetricRouter>();
                router.Route(metric);
            };
        }
    }
}
