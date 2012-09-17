namespace Statr.Server.Storage
{
    public interface IStorageEngine
    {
        IDataPointWriter GetWriter(BucketReference bucketReference);
    }
}