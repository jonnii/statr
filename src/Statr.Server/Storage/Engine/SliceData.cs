using System.Collections.Generic;
using System.Linq;

namespace Statr.Server.Storage.Engine
{
    public class SliceData
    {
        public SliceData(long startTime, float[] values)
        {
            StartTime = startTime;
            Values = values;
        }

        public long StartTime { get; private set; }

        public float[] Values { get; private set; }

        public IEnumerable<DataPoint> ToDataPoints()
        {
            return Values.Select((v, i) => new DataPoint(StartTime, v, 1));
        }
    }
}