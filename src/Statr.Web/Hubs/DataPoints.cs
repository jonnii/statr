using SignalR.Hubs;
using Statr.Client.Subscriber;

namespace Statr.Web.Hubs
{
    public class DataPoints : Hub
    {
        private readonly IDataPointSubscriber dataPointSubscriber;

        public DataPoints(IDataPointSubscriber dataPointSubscriber)
        {
            this.dataPointSubscriber = dataPointSubscriber;
            dataPointSubscriber.DataPointReceived += SubscriberOnDataPointReceived;
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