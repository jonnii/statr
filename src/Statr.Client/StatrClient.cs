using Statr.Client.Transports;

namespace Statr.Client
{
    public class StatrClient : IStatrClient
    {
        public const int DefaultPort = 17890;

        public static IStatrClient Build(string hostname)
        {
            return new StatrClient(new UdpTransport(hostname, DefaultPort));
        }

        public static IStatrClient Build(string hostname, int port)
        {
            return new StatrClient(new UdpTransport(hostname, port));
        }

        public static IStatrClient Build(IClientTransport clientTransport)
        {
            return new StatrClient(clientTransport);
        }

        private StatrClient(IClientTransport clientTransport)
        {
            Transport = clientTransport;
        }

        public IClientTransport Transport { get; private set; }

        public void Count(string bucket)
        {
            Count(bucket, 1);
        }

        public void Count(string bucket, int magnitude)
        {
            var formattedMetric =
                string.Concat(bucket, ":" + magnitude + "|c");
            Transport.Send(formattedMetric);
        }

        public void Gauge(string gaugeName, int value)
        {
            var formattedGauge =
                string.Concat(gaugeName, ":" + value + "|g");
            Transport.Send(formattedGauge);
        }

        public void Dispose()
        {
            if (Transport != null)
            {
                Transport.Dispose();
            }
        }

        public override string ToString()
        {
            return string.Concat("[StatrClient Transport=", Transport, "]");
        }
    }
}