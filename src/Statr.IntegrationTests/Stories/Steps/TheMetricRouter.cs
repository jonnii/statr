using System;
using NUnit.Framework;
using Statr.Server.Routing;

namespace Statr.IntegrationTests.Stories.Steps
{
    public static class TheMetricRouter
    {
        public static Action<StatrContext> ShouldHaveRoutedNumMetrics(int numMetrics)
        {
            return context =>
            {
                var router = context.Container.Resolve<IMetricRouter>();
                Assert.That(router.NumProcessedMetrics, Is.EqualTo(numMetrics));
            };
        }
    }
}