using Machine.Fakes;
using Machine.Specifications;
using Statr.Configuration;
using Statr.Management.Config;

namespace Statr.Specifications.Management.Config
{
    public class ConfigServiceSpecification
    {
        [Subject(typeof(ConfigService))]
        public class on_get : WithSubject<ConfigService>
        {
            Because of = () =>
                Subject.OnGet(new ConfigRequest());

            It should_get_configuration = () =>
                The<IConfigRepository>().WasToldTo(r => r.GetConfiguration());
        }
    }
}
