using System;
using System.Text;
using Castle.Core.Logging;
using Newtonsoft.Json;
using Statr.Server.Routing;
using ZMQ;

namespace Statr.Server.Publishing
{
    public class TcpPublisher : IPublisher
    {
        private readonly IDataPointStream dataPointStream;

        private IDisposable subscription;

        private Context context;

        private Socket socket;

        public TcpPublisher(IDataPointStream dataPointStream)
        {
            this.dataPointStream = dataPointStream;

            Port = 17892;
            Logger = NullLogger.Instance;
        }

        public ILogger Logger { get; set; }

        public int Port { get; set; }

        public void Start()
        {
            Logger.Info("Starting TcpPublisher");

            try
            {
                context = new Context();
                socket = context.Socket(SocketType.PUB);
                socket.Bind("tcp://*:" + Port);
            }
            catch (System.Exception e)
            {
                Logger.Fatal("Could not start publishing", e);
                throw;
            }

            subscription = dataPointStream.DataPoints.Subscribe(Publish);
        }

        public void Publish(DataPointEvent dataPointEvent)
        {
            var address = string.Concat("datapoints/", dataPointEvent.Bucket.Key);

            socket.SendMore(address, Encoding.Unicode);
            socket.Send(dataPointEvent.ToString(), Encoding.Unicode);
        }

        public void Dispose()
        {
            Logger.Info("Shutting down TcpPublisher");

            if (subscription != null)
            {
                subscription.Dispose();
            }

            if (socket != null)
            {
                socket.Dispose();
            }

            if (context != null)
            {
                context.Dispose();
            }
        }

        public string Serialize(DataPointEvent dataPointEvent)
        {
            return JsonConvert.SerializeObject(dataPointEvent);
        }
    }
}