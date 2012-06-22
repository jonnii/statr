using System;

namespace Statr.Client
{
    public interface IClientTransport : IDisposable
    {
        void Send(string metric);
    }
}