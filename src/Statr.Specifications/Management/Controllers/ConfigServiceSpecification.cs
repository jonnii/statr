using Machine.Fakes;
using Machine.Specifications;
using Statr.Configuration;
using Statr.Management.Controllers;

namespace Statr.Specifications.Management.Controllers
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
