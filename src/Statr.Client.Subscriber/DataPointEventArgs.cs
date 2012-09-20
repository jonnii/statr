using System;
using Statr.Api.Models;

namespace Statr.Client.Subscriber
{
    public class DataPointEventArgs : EventArgs
    {
        public DataPointEventArgs(string bucket, DataPoint dataPoint)
        {
            Bucket = bucket;
            DataPoint = dataPoint;
        }

        public string Bucket { get; set; }

        public DataPoint DataPoint { get; set; }
    }
}