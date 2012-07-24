using Machine.Fakes;
using Machine.Specifications;
using Statr.Management.Controllers;
using Statr.Storage;

namespace Statr.Specifications.Management.Controllers
{
    public class BucketsControllerSpecification
    {
        [Subject(typeof(BucketsController))]
        public class on_get : WithSubject<BucketsController>
        {
            Because of = () =>
                Subject.Get();

            It should_get_buckets = () =>
                The<IBucketRepository>().WasToldTo(r => r.List());
        }
    }
}
