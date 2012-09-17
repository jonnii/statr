using Statr.Api.Models;

namespace Statr.Api
{
    public interface IStatrApi
    {
        Config GetConfig();

        //IEnumerable<Bucket> GetBuckets();
    }
}
