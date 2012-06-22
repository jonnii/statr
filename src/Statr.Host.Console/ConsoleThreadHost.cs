using System;
using System.Threading;

namespace Statr.Host.Console
{
    public class ConsoleThreadHost : IDisposable
    {
        private readonly Thread hostThread;

        private StatrServerApplication serverApplication;

        public ConsoleThreadHost()
        {
            hostThread = new Thread(RunStatrServer);
            hostThread.Start();
        }

        private void RunStatrServer()
        {
            serverApplication = new StatrServerApplication();
        }

        public void Dispose()
        {
            serverApplication.Dispose();
        }
    }
}