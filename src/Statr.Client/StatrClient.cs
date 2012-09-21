using Statr.Client.Transports;

namespace Statr.Client
{
    public class StatrClient : IStatrClient
    {
        public const int DefaultPort = 17890;

        public StatrClient(string hostname)
            : this(new UdpTransport(hostname, DefaultPort))
        {

        }

        public StatrClient(string hostname, int port)
            : this(new UdpTransport(hostname, port))
        {

        }

        public StatrClient(IClientTransport clientTransport)
        {
            Transport = clientTransport;
        }

        public IClientTransport Transport { get; private set; }

        public void Count(string bucket)
        {
            Count(bucket, 1);
        }

        public void Count(string bucket, float magnitude)
        {
            var formattedMetric =
                string.Concat(bucket, ":" + magnitude + "|c");
            Transport.Send(formattedMetric);
        }

        public void Gauge(string gaugeName, float value)
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