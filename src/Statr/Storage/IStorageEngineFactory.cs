namespace Statr.Storage
{
    public interface IStorageEngineFactory
    {
        IStorageEngine Create(string rootFilePath);
    }
}