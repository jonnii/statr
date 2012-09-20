using SignalR.Hubs;

namespace Statr.Web.Hubs
{
    public class DataPointSubscriber : Hub
    {
        public void Connect(string bucket)
        {
            Groups.Add(Context.ConnectionId, bucket);
        }
    }
}