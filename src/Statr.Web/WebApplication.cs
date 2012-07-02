using System.Collections.Generic;
using Castle.MicroKernel.Registration;
using Statr.Web.Windsor;

namespace Statr.Web
{
    public class WebApplication : StatrApplication
    {
        protected override IEnumerable<IWindsorInstaller> GetInstallers()
        {
            yield return new WebInstaller();
        }
    }
}