using Topshelf;

namespace Statr.Host.Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            HostFactory.Run(x =>
            {
                x.Service<ServiceBootstrapper>(s =>
                {
                    s.SetServiceName("statr");
                    s.ConstructUsing(name => new ServiceBootstrapper());

                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });

                x.SetDescription("Sample Topshelf Host");
                x.SetDisplayName("Stuff");
                x.SetServiceName("stuff");
            });
        }
    }
}
