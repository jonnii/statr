namespace Statr.Configuration
{
    public interface IConfigService
    {
        Config GetStorageConfiguration();

        string Serialize(Config config);
    }
}
