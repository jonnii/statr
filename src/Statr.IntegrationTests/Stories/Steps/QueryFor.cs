using System;
using System.Collections.Generic;
using NUnit.Framework;
using Statr.Server;
using Statr.Server.Storage;

namespace Statr.IntegrationTests.Stories.Steps
{
    public static class QueryFor
    {
        public static Action<StatrContext> Metrics(string metricName, MetricType type, Predicate<IEnumerable<DataPoint>> predicate)
        {
            return context =>
            {
                var bucket = new BucketReference(type, metricName);

                var repository = context.Container.Resolve<IDataPointRepository>();
                var metrics = repository.Get(bucket);

                Assert.That(predicate(metrics), "The query for the metrics {0} did not pass", bucket);
            };
        }
    }
}