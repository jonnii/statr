using System.Collections.Generic;
using Statr.Routing;

namespace Statr.Storage
{
    public interface IDataPointCache
    {
        IEnumerable<DataPoint> Get(RouteKey routeKey);
    }
}