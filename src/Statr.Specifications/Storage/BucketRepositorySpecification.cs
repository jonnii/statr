using System.Collections.Generic;
using Machine.Fakes;
using Machine.Specifications;
using Statr.Storage;

namespace Statr.Specifications.Storage
{
    public class BucketRepositorySpecification
    {
        [Subject(typeof(BucketRepository))]
        public class when_getting_buckets : WithSubject<BucketRepository>
        {
            Establish context = () =>
                Subject.Get(new BucketReference("bucket.name", MetricType.Count));

            Because of = () =>
                buckets = Subject.List();

            It should_get_buckets = () =>
                buckets.ShouldNotBeEmpty();

            static IEnumerable<Bucket> buckets;
        }
    }
}