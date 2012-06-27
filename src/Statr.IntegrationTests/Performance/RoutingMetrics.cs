using System;
using System.Collections.Generic;
using System.Diagnostics;
using NUnit.Framework;
using Statr.Routing;

namespace Statr.IntegrationTests.Performance
{
    [TestFixture]
    public class RoutingMetrics : ContainerTest
    {
        [Test, Explicit]
        public void Should()
        {
            using (var container = GetContainer(c => c.LogFileName = "performance.nlog.config"))
            {
                var router = container.Resolve<IMetricRouter>();

                var watch = new Stopwatch();
                watch.Start();

                foreach (var metric in GenerateMetrics())
                {
                    router.Route(metric);
                }

                watch.Stop();

                Console.WriteLine(
                    "Processed {0} metrics in {1}ms",
                    router.NumProcessedMetrics,
                    watch.ElapsedMilliseconds);
            }
        }

        private IEnumerable<Metric> GenerateMetrics()
        {
            for (var i = 0; i < 1000000; ++i)
            {
                yield return new Metric("stats.cats", 1, MetricType.Count);
                yield return new Metric("stats.dogs", 1, MetricType.Count);
                yield return new Metric("stats.horses", 1, MetricType.Count);
                yield return new Metric("stats.fish", 1, MetricType.Count);
            }
        }
    }
}
