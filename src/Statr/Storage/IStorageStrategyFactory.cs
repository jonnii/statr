namespace Statr.Storage
{
    public interface IStorageStrategyFactory
    {
        IStorageStrategy Build();
    }
}