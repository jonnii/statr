using Machine.Fakes;
using Machine.Specifications;
using Statr.Server.Configuration;
using Statr.Server.Management.Controllers;

namespace Statr.Server.Specifications.Management.Controllers
{
    public class ConfigCSpecification
    {
        [Subject(typeof(ConfigController))]
        public class on_get : WithSubject<ConfigController>
        {
            Because of = () =>
                Subject.Get();

            It should_get_configuration = () =>
                The<IConfigRepository>().WasToldTo(r => r.GetConfiguration());
        }
    }
}
