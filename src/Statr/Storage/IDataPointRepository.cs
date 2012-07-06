using System.Collections.Generic;
using Statr.Routing;

namespace Statr.Storage
{
    public interface IDataPointRepository
    {
        IEnumerable<DataPoint> Get(Bucket bucket);
    }

}