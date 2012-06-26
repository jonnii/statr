using Castle.Core.Logging;

namespace Statr.Routing
{
    public class DataPointSubscriber : IDataPointSubscriber
    {
        public DataPointSubscriber()
        {
            Logger = NullLogger.Instance;
        }

        public ILogger Logger { get; set; }

        public void Push(DataPoint dataPoint)
        {
            Logger.InfoFormat("Received data point: {0}", dataPoint);
        }
    }
}