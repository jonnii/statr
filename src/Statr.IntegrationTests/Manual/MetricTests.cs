using NUnit.Framework;
using Statr.Configuration;
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
                var router = container.Resolve<IMetricRouter>();
                router.Route(new CountMetric("stats.cake", 30));
                router.Route(new CountMetric("stats.cake", 60));
            }
        }
    }
}