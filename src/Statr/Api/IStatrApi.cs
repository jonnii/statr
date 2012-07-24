using System.Collections.Generic;
using Statr.Configuration;

namespace Statr.Api
{
    public interface IStatrApi
    {
        Config GetConfig();

        IEnumerable<Bucket> GetBuckets();
    }
}
