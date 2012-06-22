using System;

namespace Statr.Client
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("waiting for input");
            Console.ReadKey();

            var client = StatrClient.Build("localhost");
            client.Count("some.folder.signups");
        }
    }
}
