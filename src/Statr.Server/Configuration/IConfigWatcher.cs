namespace Statr.Server.Configuration
{
    public interface IConfigWatcher
    {
        void NotifyConfigChanged(Config config);
    }
}