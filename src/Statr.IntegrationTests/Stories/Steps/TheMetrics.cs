using Statr.Server.Routing;

namespace Statr.IntegrationTests.Stories.Steps
{
    public static class TheMetrics
    {
        public static void AreFlushed(StatrContext context)
        {
            var manager = context.Container.Resolve<IMetricRouteManager>();
            manager.FlushAll();
        }
    }
}