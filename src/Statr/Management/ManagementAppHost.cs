using Funq;
using ServiceStack.WebHost.Endpoints;
using Statr.Management.Config;

namespace Statr.Management
{
    public class ManagementAppHost : AppHostHttpListenerBase
    {
        public ManagementAppHost()
            : base("Statr Management Service", typeof(ManagementAppHost).Assembly)
        {

        }

        public override void Configure(Container funqContainer)
        {
            Routes.Add<ConfigRequest>("/config");
        }
    }
}
