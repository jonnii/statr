using System;
using System.Threading;
using Statr.Client;

namespace Statr.Interactive
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Starting client");
            using (var client = StatrClient.Build("localhost"))
            {
                Console.WriteLine("Created client {0}", client);

                while (true)
                {
                    Console.WriteLine("sending stat");
                    client.Count("stats.ticker");
                    Thread.Sleep(1000);
                }
            }
        }
    }
}
