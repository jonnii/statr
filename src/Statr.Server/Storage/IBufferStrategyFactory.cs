namespace Statr.Storage
{
    public interface IBufferStrategyFactory
    {
        IBufferStrategy Build(BucketReference bucketReference);
    }
}