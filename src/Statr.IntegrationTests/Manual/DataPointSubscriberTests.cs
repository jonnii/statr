using System.Threading;
using NUnit.Framework;
using Statr.Client.Subscriber;

namespace Statr.IntegrationTests.Manual
{
    [TestFixture]
    public class DataPointSubscriberTests
    {
        [Test, Explicit]
        public void Should()
        {
            using (var subscriber = new DataPointSubscriber("localhost", 17892))
            {
                subscriber.Start();
                Thread.Sleep(50000);
            }
        }
    }
}
