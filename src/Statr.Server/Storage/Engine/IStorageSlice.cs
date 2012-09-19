namespace Statr.Server.Storage.Engine
{
    public interface IStorageSlice
    {
        void Write(SliceData sliceData);

        SliceData Read();
    }
}