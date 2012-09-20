using System.Collections.Generic;
using Statr.Api.Models;

namespace Statr.Api
{
    public interface IStatrApi
    {
        Config Config();

        IEnumerable<Bucket> Buckets();

        IEnumerable<DataPoint> DataPoints(string metricType, string bucket);
    }
}
