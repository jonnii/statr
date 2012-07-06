using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Statr.Storage
{
    public class DataPointCollection : IEnumerable<DataPoint>
    {
        private readonly IList<DataPoint> dataPoints;

        public DataPointCollection()
        {
            dataPoints = new List<DataPoint>();
        }

        public DataPointCollection(IList<DataPoint> dataPoints)
        {
            this.dataPoints = dataPoints;
        }

        public DataPointCollection Add(DataPoint point)
        {
            dataPoints.Add(point);

            return this;
        }

        public IEnumerator<DataPoint> GetEnumerator()
        {
            return dataPoints.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public SliceData ToSliceData()
        {
            var startTime = dataPoints.First().TimeStamp;
            var dataPointValues = dataPoints.Select(d => d.Value.Value).ToArray();

            return new SliceData(startTime, dataPointValues);
        }
    }
}