using System;
using System.Linq;

namespace Statr.Server.Specifications.Fixtures
{
    public static class DataPointFixture
    {
        public static DataPoint Create(params Action<DataPoint>[] builders)
        {
            var config = new DataPoint(DateTime.Now, 500, 1);

            foreach (var builder in builders)
            {
                builder(config);
            }

            return config;
        }

        public static DataPoint[] CreateMany(int num, params Action<DataPoint>[] builders)
        {
            return Enumerable.Repeat(Create(builders), num).ToArray();
        }
    }
}