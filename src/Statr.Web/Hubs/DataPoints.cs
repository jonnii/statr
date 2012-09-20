using SignalR.Hubs;
using Statr.Client.Subscriber;

namespace Statr.Web.Hubs
{
    public class DataPoints : Hub
    {
        public DataPoints()
        {
            var subscriber = new DataPointSubscriber("localhost", 17892);
            subscriber.Start();
            subscriber.DataPointReceived += SubscriberOnDataPointReceived;
        }

        public void Connect(string bucket)
        {
            Groups.Add(Context.ConnectionId, bucket);
            Caller.ack(bucket);
        }

        private void SubscriberOnDataPointReceived(object sender, DataPointEventArgs dataPointEventArgs)
        {
            Clients[dataPointEventArgs.Bucket].receive(dataPointEventArgs.DataPoint);
        }
    }
}