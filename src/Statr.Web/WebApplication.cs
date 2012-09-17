using System.Collections.Generic;
using System.Web.Http;
using Castle.MicroKernel.Registration;
using Statr.Web.Windsor;

namespace Statr.Web
{
    public class WebApplication : StatrApplication
    {
        private readonly HttpConfiguration httpConfiguration;

        public WebApplication(HttpConfiguration httpConfiguration)
        {
            this.httpConfiguration = httpConfiguration;
        }

        protected override IEnumerable<IWindsorInstaller> GetInstallers()
        {
            yield return new WebInstaller(httpConfiguration);
        }
    }
}