namespace Statr.Storage
{
    public class SliceData
    {
        public SliceData(long startTime, long[] dataPoints)
        {
            StartTime = startTime;
            DataPoints = dataPoints;
        }

        public long StartTime { get; private set; }

        public long[] DataPoints { get; private set; }
    }
}