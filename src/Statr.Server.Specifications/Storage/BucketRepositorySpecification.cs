using System.Collections.Generic;
using Machine.Fakes;
using Machine.Specifications;
using Statr.Server.Storage;

namespace Statr.Server.Specifications.Storage
{
    public class BucketRepositorySpecification
    {
        [Subject(typeof(BucketRepository))]
        public class when_getting_buckets : with_buckets
        {
            Because of = () =>
                buckets = Subject.List();

            It should_get_buckets = () =>
                buckets.ShouldNotBeEmpty();

            static IEnumerable<Bucket> buckets;
        }

        public class with_buckets : WithSubject<BucketRepository>
        {
            Establish context = () =>
                Subject.Get(new BucketReference("bucket.name", MetricType.Count));
        }
    }
}