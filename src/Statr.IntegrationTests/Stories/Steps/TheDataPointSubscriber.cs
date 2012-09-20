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

                while (!subscriber.IsSubscribed)
                {
                    Thread.Sleep(10);
                }
                Thread.Sleep(10);

                context.Add(subscriber);
            };
        }

        public static Action<StatrContext> ShouldHaveReceivedMetrics(int numMetrics)
        {
            return context =>
            {
                var subscriber = context.Get<DataPointSubscriber>();
                Thread.Sleep(500);
                Assert.That(subscriber.NumReceivedDataPoints, Is.EqualTo(1));
            };
        }
    }
}
