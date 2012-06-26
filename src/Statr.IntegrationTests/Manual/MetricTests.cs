using System.Threading;
using NUnit.Framework;
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
                var metricRouter = container.Resolve<IMetricRouter>();
                metricRouter.Route(new CountMetric("stats.cake", 30));
                metricRouter.Route(new CountMetric("stats.cake", 20));
                metricRouter.Route(new CountMetric("stats.cake", 40));
                metricRouter.Route(new CountMetric("stats.cake", 60));

                Thread.Sleep(10000);
            }
        }
    }
}