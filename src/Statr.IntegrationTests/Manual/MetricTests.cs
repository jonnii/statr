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
                metricRouter.Route(new Metric("stats.cake", 30, MetricType.Count));
                metricRouter.Route(new Metric("stats.cake", 20, MetricType.Count));
                metricRouter.Route(new Metric("stats.cake", 40, MetricType.Count));
                metricRouter.Route(new Metric("stats.cake", 60, MetricType.Count));
            }
        }
    }
}