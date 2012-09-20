using System;
using System.Text;
using Castle.Core.Logging;
using Statr.Server.Routing;
using ZMQ;

namespace Statr.Server.Publishing
{
    public class Publisher : IPublisher
    {
        private readonly IDataPointStream dataPointStream;

        private IDisposable subscription;

        private Context context;

        private Socket socket;

        public Publisher(IDataPointStream dataPointStream)
        {
            this.dataPointStream = dataPointStream;

            Port = 17892;
            Logger = NullLogger.Instance;
        }

        public ILogger Logger { get; set; }

        public int Port { get; set; }

        public void Start()
        {
            Logger.Info("Starting Publisher");

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
            Logger.Info("Publishing data point");

            socket.SendMore("message", Encoding.Unicode);
            socket.Send(dataPointEvent.ToString(), Encoding.Unicode);
        }

        public void Dispose()
        {
            Logger.Info("Shutting down Publisher");

            if (subscription != null)
            {
                subscription.Dispose();
            }

            if (socket != null)
            {
                try
                {
                    socket.Dispose();
                }
                catch { }
            }

            if (context != null)
            {
                try
                {
                    context.Dispose();
                }
                catch { }
            }
        }
    }
}