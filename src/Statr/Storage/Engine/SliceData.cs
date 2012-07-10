namespace Statr.Storage.Engine
{
    public class SliceData
    {
        public SliceData(long startTime, float[] dataPoints)
        {
            StartTime = startTime;
            DataPoints = dataPoints;
        }

        public long StartTime { get; private set; }

        public float[] DataPoints { get; private set; }
    }
}