using Castle.Core.Logging;
using Statr.Server.Storage;

namespace Statr.Server.Querying
{
    public class QueryEngine : IQueryEngine
    {
        private readonly IDataPointCache dataPointCache;

        public QueryEngine(IDataPointCache dataPointCache)
        {
            this.dataPointCache = dataPointCache;

            Logger = NullLogger.Instance;
        }

        public ILogger Logger { get; set; }

        public QueryResult Execute(Query query)
        {
            Logger.DebugFormat("Executing query: {0}", query);

            var dataPoints = dataPointCache.Get(query.BucketReference);

            return new QueryResult(dataPoints);
        }
    }
}