using Statr.Configuration;

namespace Statr.Server.Configuration
{
    public interface IConfigRepository
    {
        Config GetConfiguration();

        string Serialize(Config config);

        void WriteConfiguration(Config config);
    }
}
