using System;
using System.Threading;
using Statr.Client;

namespace Statr.Interactive
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Waiting for keypress...");
            Console.ReadKey();

            Console.WriteLine("Starting client");
            using (var client = StatrClient.Build("localhost"))
            {
                while (true)
                {
                    client.Count("stats.ticker");
                    Thread.Sleep(1000);
                }
            }
        }
    }
}
