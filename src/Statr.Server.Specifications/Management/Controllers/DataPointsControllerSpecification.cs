using Machine.Fakes;
using Machine.Specifications;
using Statr.Server.Management.Controllers;
using Statr.Server.Storage;

namespace Statr.Server.Specifications.Management.Controllers
{
    public class DataPointsControllerSpecification
    {
        [Subject(typeof(DataPointsController))]
        public class on_get : WithSubject<DataPointsController>
        {
            Because of = () =>
                Subject.Get("Count", "bucket.id");

            It should_get_data_points_from_cache = () =>
                The<IDataPointCache>().WasToldTo(c => c.Get(Param.IsAny<BucketReference>()));
        }
    }
}
