using System.Text.RegularExpressions;
using Statr.Config;

namespace Statr.Routing
{
    public class RouteDefinition
    {
        public RouteDefinition(StorageEntry storage)
        {
            Storage = storage;
        }

        public StorageEntry Storage { get; set; }

        public bool AppliesTo(string metricName)
        {
            return Regex.IsMatch(metricName, Storage.Pattern);
        }
    }
}