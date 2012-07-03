namespace Statr.Configuration
{
    public interface IConfigRepository
    {
        Config GetConfiguration();

        string Serialize(Config config);

        void WriteConfiguration(Config config);
    }
}
