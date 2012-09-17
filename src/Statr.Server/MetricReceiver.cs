using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Castle.Core.Logging;
using Statr.Server.Routing;

namespace Statr.Server
{
    public class MetricReceiver : IMetricReceiver
    {
        private readonly IMetricParser metricParser;

        private readonly IMetricRouter metricRouter;

        private bool shouldReceive = true;

        private Thread receiveThread;

        public MetricReceiver(
            IMetricParser metricParser,
            IMetricRouter metricRouter)
        {
            this.metricParser = metricParser;
            this.metricRouter = metricRouter;

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

            Logger.DebugFormat("Starting MetricReceiver on port {0}", Port);

            receiveThread = new Thread(InitializeServer);
            receiveThread.Start();

            Logger.DebugFormat("MetricReceiver started");
        }

        public void InitializeServer()
        {
            var endPoint = new IPEndPoint(IPAddress.Any, Port);
            var udpClient = new UdpClient(endPoint);

            while (shouldReceive)
            {
                var sender = new IPEndPoint(IPAddress.Any, 0);
                var data = udpClient.Receive(ref sender);

                var rawMetric = Encoding.ASCII.GetString(data, 0, data.Length);
                var trimmed = rawMetric.Trim();
                Logger.DebugFormat("{0} => {1}", sender, trimmed);

                var metric = metricParser.Parse(trimmed);

                metricRouter.Route(metric);
            }
        }

        public void Dispose()
        {
            shouldReceive = false;
        }
    }
}
