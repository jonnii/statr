using System;
using Statr.Routing;

namespace Statr
{
    public class DataPointEventArgs : EventArgs
    {
        public DataPointEventArgs(RouteKey routeKey, DataPoint dataPoint)
        {
            RouteKey = routeKey;
            DataPoint = dataPoint;
        }

        public RouteKey RouteKey { get; private set; }

        public DataPoint DataPoint { get; private set; }
    }
}