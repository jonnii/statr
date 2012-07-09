namespace Statr.Storage
{
    public interface IStorageNode
    {
        string FilePath { get; }

        void Store(DataPointCollection points);

        IStorageSlice CreateSlice(long startTime, int timeStep);
    }
}