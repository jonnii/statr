using System;
using NUnit.Framework;
using Statr.Storage;

namespace Statr.IntegrationTests.Stories.Steps
{
    public static class TheBucket
    {
        public static BucketQuery For(string name, MetricType metricType)
        {
            return new BucketQuery(name, metricType);
        }

        public class BucketQuery
        {
            private readonly string name;

            private readonly MetricType metricType;

            public BucketQuery(string name, MetricType metricType)
            {
                this.name = name;
                this.metricType = metricType;
            }

            public Action<StatrContext> ShouldHave(Predicate<Bucket> predicate)
            {
                return context =>
                {   
                    var bucketRepository = context.Container.Resolve<IBucketRepository>();
                    var bucket = bucketRepository.Get(new BucketReference(name, metricType));

                    Assert.That(predicate(bucket), Is.True, "Bucket did not pass predicate");
                };
            }
        }
    }
}
