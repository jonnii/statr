using Machine.Fakes;
using Machine.Specifications;
using Statr.Api;
using Statr.Web.Controllers;

namespace Statr.Web.Specifications.Controllers
{
    public class ConfigurationControllerSpecification
    {
        [Subject(typeof(ConfigurationController))]
        public class on_get_index : WithSubject<ConfigurationController>
        {
            Because of = () =>
                Subject.Index();

            It should_get_config = () =>
                The<IStatrApi>().WasToldTo(a => a.GetConfig());
        }
    }
}
