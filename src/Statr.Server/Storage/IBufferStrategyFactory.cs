namespace Statr.Server.Storage
{
    public interface IBufferStrategyFactory
    {
        IBufferStrategy Build(BucketReference bucketReference);
    }
}