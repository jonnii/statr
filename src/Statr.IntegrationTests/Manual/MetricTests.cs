using System;
using System.Linq;
using System.Reactive.Linq;
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
            var container = GetContainer();

            var router = container.Resolve<IMetricRouter>();
            var subscription = router.RegisterRoute(new RouteDefinition("^stats"));

            var observable = Observable.FromEventPattern<EventHandler<MetricEventArgs>, MetricEventArgs>(
              h => subscription.MetricReceived += h,
              h => subscription.MetricReceived -= h);

            observable
                .Buffer(TimeSpan.FromSeconds(1))
                .Subscribe(o =>
                {
                    var metrics = o
                        .Select(e => e.EventArgs.Metric)
                        .Cast<CountMetric>();

                    var numEvents = metrics.Sum(e => e.Amount);
                    Console.WriteLine(numEvents);
                });

            Thread.Sleep(1000);
            subscription.NotifyMetric(new CountMetric("fribble", 5));
            subscription.NotifyMetric(new CountMetric("fribble", 5));
            Thread.Sleep(2000);
            subscription.NotifyMetric(new CountMetric("fribble", 5));
            subscription.NotifyMetric(new CountMetric("fribble", 5));
            subscription.NotifyMetric(new CountMetric("fribble", 5));
            Thread.Sleep(5000);
            subscription.NotifyMetric(new CountMetric("fribble", 5));
            subscription.NotifyMetric(new CountMetric("fribble", 5));
            Thread.Sleep(2000);
        }
    }
}