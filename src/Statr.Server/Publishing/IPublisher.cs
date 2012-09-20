using System;

namespace Statr.Server.Publishing
{
    public interface IPublisher : IDisposable
    {
        void Start();
    }
}
