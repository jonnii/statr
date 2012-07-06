using System.Collections.Generic;
using Statr.Configuration;
using Statr.Routing;

namespace Statr.Api
{
    public interface IStatrApi
    {
        Config GetConfig();

        IEnumerable<Bucket> GetBuckets();
    }
}
