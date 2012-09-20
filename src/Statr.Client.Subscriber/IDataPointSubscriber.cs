using System;

namespace Statr.Client.Subscriber
{
    public interface IDataPointSubscriber : IDisposable
    {
        event EventHandler<DataPointEventArgs> DataPointReceived;

        long NumReceivedDataPoints { get; }

        void Start();
    }
}