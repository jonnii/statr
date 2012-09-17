namespace Statr.Storage
{
    public interface IStorageEngine
    {
        IDataPointWriter GetWriter(BucketReference bucketReference);
    }
}