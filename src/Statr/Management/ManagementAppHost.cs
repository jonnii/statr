using Funq;
using ServiceStack.WebHost.Endpoints;

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
        }
    }
}
