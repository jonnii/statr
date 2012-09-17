using System;

namespace Statr
{
    public interface IMetricReceiver : IDisposable
    {
        void Start();
    }
}