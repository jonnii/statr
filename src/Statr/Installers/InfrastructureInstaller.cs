using System;
using Castle.Facilities.Logging;
using Castle.Facilities.Startable;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using FluentValidation;
using Statr.Infrastructure;

namespace Statr.Installers
{
    public class InfrastructureInstaller : IWindsorInstaller
    {
        public static Type[] anchors = new[]
        {
            typeof(NLog.LogFactory),
            typeof(Castle.Services.Logging.NLogIntegration.NLogFactory)
        };

        private readonly string logFileName;

        public InfrastructureInstaller(string logFileName)
        {
            this.logFileName = logFileName;
        }

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Kernel.Resolver.AddSubResolver(new ArrayResolver(container.Kernel));

            container.AddFacility(new StartableFacility());
            container.AddFacility<LoggingFacility>(f => f.UseNLog().WithConfig(logFileName ?? "nlog.config"));
            container.AddFacility<TypedFactoryFacility>();

            container.Register(
                Component.For<IFileSystem>().ImplementedBy<FileSystem>(),
                Classes.FromThisAssembly().BasedOn(typeof(IValidator<>)).WithService.FromInterface(typeof(IValidator<>)));
        }
    }
}
