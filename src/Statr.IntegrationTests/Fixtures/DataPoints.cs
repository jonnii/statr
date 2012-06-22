using System;
using System.Linq;
using Statr.Storage;

namespace Statr.IntegrationTests.Fixtures
{
    public class DataPoints
    {
        public static DataPointCollection Create(int numPoints = 100)
        {
            var points = Enumerable.Range(0, numPoints)
                .Reverse()
                .Select(i => new DataPoint(DateTime.Now.AddSeconds(-i), i))
                .ToArray();

            return new DataPointCollection(points);
        }
    }
}