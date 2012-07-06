using System;
using System.Collections.Generic;
using NUnit.Framework;
using Statr.Routing;
using Statr.Storage;

namespace Statr.IntegrationTests.Stories.Steps
{
    public static class QueryFor
    {
        public static Action<StatrContext> Metrics(string metricName, MetricType type, Predicate<IEnumerable<DataPoint>> predicate)
        {
            return context =>
            {
                var routeKey = new RouteKey(metricName, type);

                var repository = context.Container.Resolve<IDataPointRepository>();
                var metrics = repository.Get(routeKey);

                Assert.That(predicate(metrics), "The query for the metrics {0} did not pass", routeKey);
            };
        }
    }
}