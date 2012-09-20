﻿using System;
using System.Text;
using System.Threading;
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

        public long NumReceivedDataPoints { get; private set; }

        public bool IsSubscribed { get; private set; }

        public void Start()
        {
            thread = new Thread(StartReceiving);
            thread.Start();
        }

        public void StartReceiving()
        {
            using (var context = new Context())
            {
                using (var subscriber = context.Socket(SocketType.SUB))
                {
                    subscriber.Linger = 0;

                    subscriber.Connect(string.Format("tcp://{0}:{1}", server, port));
                    subscriber.Subscribe("datapoints/", Encoding.Unicode);

                    IsSubscribed = true;

                    while (!isDisposed)
                    {
                        var address = subscriber.Recv(Encoding.Unicode, 10);
                        var contents = subscriber.Recv(Encoding.Unicode, 10);

                        if (string.IsNullOrEmpty(address) || string.IsNullOrEmpty(contents))
                        {
                            Thread.Sleep(10);
                            continue;
                        }

                        Console.WriteLine("{0} : {1}", address, contents);

                        ++NumReceivedDataPoints;
                    }
                }
            }
        }

        public void Dispose()
        {
            isDisposed = true;
        }
    }
}
