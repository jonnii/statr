namespace Statr.Storage.Engine
{
    public interface IStorageSlice
    {
        void Write(SliceData sliceData);

        long[] Read();
    }
}