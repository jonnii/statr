using Statr.Client;
using Statr.Server;
using Statr.Server.Routing;

namespace Statr.IntegrationTests.Support
{
    public class DirectClientTransport : IClientTransport
    {
        private readonly IMetricParser parser;

        private readonly IMetricRouter router;

        public DirectClientTransport(
            IMetricParser parser,
            IMetricRouter router)
        {
            this.parser = parser;
            this.router = router;
        }

        public void Send(string metric)
        {
            var parsed = parser.Parse(metric);
            router.Route(parsed);
        }

        public void Dispose()
        {

        }
    }
}
