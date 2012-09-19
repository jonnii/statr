using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using Statr.Client;
using Statr.Interactive;
using Statr.Server.Routing;

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
                var client = container.Resolve<IStatrClient>();
                var generator = container.Resolve<IMetricsGenerator>(new { client });
                var router = container.Resolve<IMetricRouter>();

                var watch = new Stopwatch();
                watch.Start();

                var requests = GenerateMetrics(200);
                var tasks = requests.Select(generator.SendMetrics);

                Task.WaitAll(tasks.ToArray());

                watch.Stop();

                Console.WriteLine(
                    "Processed {0} metrics in {1}ms",
                    router.NumProcessedMetrics,
                    watch.ElapsedMilliseconds);
            }
        }

        private IEnumerable<GeneratorRequest> GenerateMetrics(int numMetrics)
        {
            for (var i = 0; i < 60; ++i)
            {
                yield return new GeneratorRequest("stats.dogs", "count", numMetrics, new Range(1, 1), new Range(500, 1000));
                yield return new GeneratorRequest("stats.cats", "count", numMetrics, new Range(1, 1), new Range(500, 1000));
                yield return new GeneratorRequest("stats.horses", "count", numMetrics, new Range(1, 1), new Range(500, 1000));
                yield return new GeneratorRequest("stats.cakes", "count", numMetrics, new Range(1, 1), new Range(500, 1000));
            }
        }
    }
}
