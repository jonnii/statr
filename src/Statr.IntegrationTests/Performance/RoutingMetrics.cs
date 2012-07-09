using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using Statr.Client;
using Statr.Interactive;
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
                var client = container.Resolve<IStatrClient>();
                var generator = container.Resolve<IMetricsGenerator>(new { client });
                var router = container.Resolve<IMetricRouter>();

                var watch = new Stopwatch();
                watch.Start();

                var requests = GenerateMetrics();
                var tasks = requests.Select(generator.SendMetrics);

                Task.WaitAll(tasks.ToArray());

                watch.Stop();

                Console.WriteLine(
                    "Processed {0} metrics in {1}ms",
                    router.NumProcessedMetrics,
                    watch.ElapsedMilliseconds);
            }
        }

        private IEnumerable<GeneratorRequest> GenerateMetrics()
        {
            yield return new GeneratorRequest("stats.dogs", "count", 1000, new Range(5, 10), new Range(500, 1000));
            yield return new GeneratorRequest("stats.dogs", "count", 1000, new Range(5, 10), new Range(500, 1000));
            yield return new GeneratorRequest("stats.dogs", "count", 1000, new Range(5, 10), new Range(500, 1000));
            yield return new GeneratorRequest("stats.dogs", "count", 1000, new Range(5, 10), new Range(500, 1000));
            yield return new GeneratorRequest("stats.dogs", "count", 100, new Range(50, 100), new Range(500, 1000));

            yield return new GeneratorRequest("stats.cats", "count", 1000, new Range(5, 10), new Range(500, 1000));
            yield return new GeneratorRequest("stats.cats", "count", 1000, new Range(5, 10), new Range(500, 1000));
            yield return new GeneratorRequest("stats.cats", "count", 1000, new Range(5, 10), new Range(500, 1000));
            yield return new GeneratorRequest("stats.cats", "count", 100, new Range(50, 100), new Range(500, 1000));

            yield return new GeneratorRequest("stats.horses", "count", 100, new Range(50, 100), new Range(500, 1000));
        }
    }
}
