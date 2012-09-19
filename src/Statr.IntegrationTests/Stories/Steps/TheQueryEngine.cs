using System;
using Statr.Server;
using Statr.Server.Querying;

namespace Statr.IntegrationTests.Stories.Steps
{
    public static class TheQueryEngine
    {
        public static Action<StatrContext> Fetches(string bucket, MetricType metricType)
        {
            return context =>
            {
                var query = new Query(bucket, metricType);

                var queryEngine = context.QueryEngine;
                var results = queryEngine.Execute(query);

                context.Add(results.DataPoints);
            };
        }
    }
}
