using System;
using System.Net.Sockets;
using System.Text;

namespace Statr.Client.Transports
{
    public class UdpTransport : IClientTransport
    {
        private readonly string host;

        private readonly int port;

        private readonly Lazy<UdpClient> udpClient;

        public UdpTransport(string host, int port)
        {
            this.host = host;
            this.port = port;

            udpClient = new Lazy<UdpClient>(CreateUdpClient);
        }

        private UdpClient CreateUdpClient()
        {
            return new UdpClient(host, port);
        }

        public void Send(string metric)
        {
            var data = Encoding.Default.GetBytes(metric + "\n");
            udpClient.Value.Send(data, data.Length);
        }

        public void Dispose()
        {
            if (udpClient.IsValueCreated)
            {
                udpClient.Value.Close();
            }
        }
    }
}