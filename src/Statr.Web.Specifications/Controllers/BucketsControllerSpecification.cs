using Machine.Fakes;
using Machine.Specifications;
using Statr.Api;
using Statr.Web.Controllers;

namespace Statr.Web.Specifications.Controllers
{
    public class BucketsControllerSpecification
    {
        [Subject(typeof(BucketsController))]
        public class on_index : WithSubject<BucketsController>
        {
            Because of = () =>
                Subject.Index();

            It should_get_metrics = () =>
                The<IStatrApi>().WasToldTo(a => a.GetBuckets());
        }
    }
}