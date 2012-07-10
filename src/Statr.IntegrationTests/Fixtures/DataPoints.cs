using System;
using System.Collections.Generic;
using System.Linq;

namespace Statr.IntegrationTests.Fixtures
{
    public class DataPoints
    {
        public static IEnumerable<DataPoint> Create(int numPoints = 100)
        {
            return Enumerable.Range(0, numPoints)
                .Reverse()
                .Select(i => new DataPoint(DateTime.Now.AddSeconds(-i), i, 1));
        }
    }
}