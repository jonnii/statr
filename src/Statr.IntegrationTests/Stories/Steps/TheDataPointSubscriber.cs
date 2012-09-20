using System;
using System.Threading;
using NUnit.Framework;
using Statr.Client.Subscriber;

namespace Statr.IntegrationTests.Stories.Steps
{
    public static class TheDataPointSubscriber
    {
        public static Action<StatrContext> IsListening()
        {
            return context =>
            {
                var subscriber = new DataPointSubscriber("localhost", 17892);
                subscriber.Start();

                context.Add(subscriber);
            };
        }

        public static Action<StatrContext> ShouldHaveReceivedMetrics(int numMetrics)
        {
            return context =>
            {
                Thread.Sleep(2000);

                var subscriber = context.Get<DataPointSubscriber>();
                Assert.That(subscriber.NumReceivedDataPoints, Is.EqualTo(1));
            };
        }
    }
}
