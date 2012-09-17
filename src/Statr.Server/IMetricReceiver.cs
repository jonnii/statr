using System;

namespace Statr.Server
{
    public interface IMetricReceiver : IDisposable
    {
        void Start();
    }
}