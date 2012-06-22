namespace Statr.Storage
{
    public interface IStorageSlice
    {
        void Write(SliceData sliceData);

        long[] Read();
    }
}