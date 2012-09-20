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

        public long NumPublishedMessage { get; private set; }

        public void Start()
        {
            Logger.Info("Starting TcpPublisher");

            try
            {
                context = new Context();

                socket = context.Socket(SocketType.PUB);
                socket.Linger = 0;

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

            ++NumPublishedMessage;
        }

        public void Dispose()
        {
            Logger.InfoFormat("Disposing TcpPublisher ({0} published messages)", NumPublishedMessage);

            if (subscription != null)
            {
                Logger.Info(" => Disposing subscription");
                subscription.Dispose();
            }

            if (socket != null)
            {
                Logger.Info(" => Disposing socket");
                socket.Dispose();
            }

            if (context != null)
            {
                Logger.Info(" => Disposing context");
                context.Dispose();
            }
        }

        public string Serialize(DataPointEvent dataPointEvent)
        {
            return JsonConvert.SerializeObject(dataPointEvent);
        }
    }
}