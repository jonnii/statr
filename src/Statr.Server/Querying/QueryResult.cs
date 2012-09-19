using System.Collections.Generic;

namespace Statr.Server.Querying
{
    public class QueryResult
    {
        public QueryResult(IEnumerable<DataPoint> dataPoints)
        {
            DataPoints = dataPoints;
        }

        public IEnumerable<DataPoint> DataPoints { get; private set; }
    }
}