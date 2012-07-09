using Statr.Storage.Strategies;

namespace Statr.Storage
{
    public class StorageStrategyFactory : IStorageStrategyFactory
    {
        public IStorageStrategy Build()
        {
            return new ImmediateStorageStrategy();
        }
    }
}