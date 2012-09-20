using System;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using Statr.Api.Models;
using ZMQ;

namespace Statr.Client.Subscriber
{
    public class DataPointSubscriber : IDisposable
    {
        private readonly string server;

        private readonly int port;

        private bool isDisposed;

        private Thread thread;

        public DataPointSubscriber(string server, int port)
        {
            this.server = server;
            this.port = port;
        }

        public event EventHandler<DataPointEventArgs> DataPointReceived;

        public long NumReceivedDataPoints { get; private set; }

        public bool IsSubscribed { get; private set; }

        public void Start()
        {
            thread = new Thread(StartReceiving);
            thread.Start();
        }

        public void StartReceiving()
        {
            using (var context = new Context(1))
            {
                using (var subscriber = context.Socket(SocketType.SUB))
                {
                    subscriber.Connect(string.Format("tcp://{0}:{1}", server, port));
                    subscriber.Subscribe("datapoints/", Encoding.Unicode);

                    IsSubscribed = true;

                    while (!isDisposed)
                    {
                        var address = subscriber.Recv(Encoding.Unicode, 5);
                        var contents = subscriber.Recv(Encoding.Unicode, 5);

                        if (string.IsNullOrEmpty(address) || string.IsNullOrEmpty(contents))
                        {
                            Thread.Sleep(5);
                            continue;
                        }

                        var dataPoint = JsonConvert.DeserializeObject<DataPoint>(contents);

                        OnDataPointEvent(address, dataPoint);

                        ++NumReceivedDataPoints;
                    }
                }
            }

            IsSubscribed = false;
        }

        private void OnDataPointEvent(string bucket, DataPoint dataPointEvent)
        {
            var handler = DataPointReceived;
            if (handler != null)
            {
                handler(this, new DataPointEventArgs(bucket, dataPointEvent));
            }
        }

        public void Dispose()
        {
            isDisposed = true;
        }
    }
}
