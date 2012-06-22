using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Castle.Core.Logging;

namespace Statr
{
    public class MetricReceiver : IMetricReceiver
    {
        private bool shouldReceive = true;

        private Thread receiveThread;

        public MetricReceiver()
        {
            Logger = NullLogger.Instance;
        }

        public ILogger Logger { get; set; }

        public int Port { get; set; }

        public void Start()
        {
            if (Port == 0)
            {
                throw new StatrException("The server cannot be started because the port has not been set");
            }

            Logger.DebugFormat("Starting server on port {0}", Port);

            receiveThread = new Thread(InitializeServer);
            receiveThread.Start();
        }

        public void InitializeServer()
        {
            var endPoint = new IPEndPoint(IPAddress.Any, Port);
            var udpClient = new UdpClient(endPoint);

            while (shouldReceive)
            {
                var sender = new IPEndPoint(IPAddress.Any, 0);
                var data = udpClient.Receive(ref sender);



                Logger.DebugFormat("Message received from {0}:", sender);
                Logger.DebugFormat(Encoding.ASCII.GetString(data, 0, data.Length));
            }
        }

        public void Dispose()
        {
            shouldReceive = false;
        }
    }
}
