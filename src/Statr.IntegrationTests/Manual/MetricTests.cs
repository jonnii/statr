using NUnit.Framework;
using Statr.Config;
using Statr.Routing;

namespace Statr.IntegrationTests.Manual
{
    [TestFixture]
    public class MetricTests : ContainerTest
    {
        [Test, Explicit]
        public void ShouldSave()
        {
            using (var container = GetContainer())
            {
                var registry = container.Resolve<IMetricRouteRegistry>();
                registry.RegisterRoute(
                    new RouteDefinition(new StorageEntry("all stats", "^stats", new Retention("1m", "15m"))));

                var router = container.Resolve<IMetricRouter>();
                router.Route(new CountMetric("stats.cake", 30));
                router.Route(new CountMetric("stats.cake", 60));


            }
        }
    }
}