using System;
using Statr.Client;

namespace Statr.Interactive
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var client = StatrClient.Build("127.0.0.1"))
            {
                Console.WriteLine("!!! Created client {0}", client);
                var interactive = new MetricsGenerator(client);
                StartInputLoop(interactive);
            }
        }

        private static void ShowInstructions()
        {
            Console.WriteLine("Instructions");
            Console.WriteLine("------------");
            Console.WriteLine();
            Console.WriteLine("send metrics \t\t <name> <type> <num> <interval-range> <value-range>");
            Console.WriteLine("q - quit");
            Console.WriteLine();
            Console.WriteLine();
        }

        private static void ShowWelcomeBanner()
        {
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("| Welcome to the Statr Interactive Console |");
            Console.WriteLine("--------------------------------------------");

            Console.WriteLine();
            Console.WriteLine();
        }

        public static void StartInputLoop(IMetricsGenerator generator)
        {
            ShowWelcomeBanner();

            while (true)
            {
                ShowInstructions();

                var currentLine = Console.ReadLine();

                if (string.IsNullOrEmpty(currentLine))
                {
                    continue;
                }

                if (currentLine.StartsWith("q"))
                {
                    break;
                }

                Console.WriteLine(currentLine);

                try
                {
                    generator.SendMetrics(currentLine)
                        .ContinueWith(_ => Console.WriteLine("Finished sending metrics: {0}", currentLine));
                }
                catch (FormatException fe)
                {
                    Console.WriteLine("oops, you did something wrong...");
                    Console.WriteLine(fe.Message);
                }
            }

            Console.WriteLine("Thanks for playing! Bye!");
        }
    }
}
