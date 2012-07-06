using Machine.Fakes;
using Machine.Specifications;
using Statr.Management.Buckets;
using Statr.Storage;

namespace Statr.Specifications.Management.Buckets
{
    public class BucketsServiceSpecification
    {
        [Subject(typeof(BucketsService))]
        public class on_get : WithSubject<BucketsService>
        {
            Establish context = () => { };

            Because of = () =>
                Subject.OnGet(new BucketsRequest());

            It should_get_buckets = () =>
                The<IBucketRepository>().WasToldTo(r => r.List());
        }
    }
}
