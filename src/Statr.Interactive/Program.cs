using System;
using System.Reactive.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Statr.Client;

namespace Statr.Interactive
{
    public class Program
    {
        public static void Main(string[] args)
        {

            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("| Welcome to the Statr Interactive Console |");
            Console.WriteLine("--------------------------------------------");

            Console.WriteLine();
            Console.WriteLine();

            using (var client = StatrClient.Build("127.0.0.1"))
            {
                Console.WriteLine("!!! Created client {0}", client);

                while (true)
                {
                    Console.WriteLine("Instructions");
                    Console.WriteLine("------------");
                    Console.WriteLine();
                    Console.WriteLine("send metrics \t\t s <name> <type> <num> <interval> <value>");
                    Console.WriteLine("send metrics (thread) \t s! <name> <type> <num> <interval> <value>");
                    Console.WriteLine("q - quit");

                    var currentLine = Console.ReadLine();

                    if (string.IsNullOrEmpty(currentLine))
                    {
                        continue;
                    }

                    if (currentLine.StartsWith("q"))
                    {
                        break;
                    }

                    if (currentLine.StartsWith("s"))
                    {
                        try
                        {
                            SendMetrics(client, currentLine);
                        }
                        catch (FormatException fe)
                        {
                            Console.WriteLine("oops, you did something wrong...");
                            Console.WriteLine(fe.Message);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Unknown command: {0}", currentLine);
                    }
                }
            }

            Console.WriteLine("Thanks for playing! Bye!");
        }

        private static void SendMetrics(IStatrClient client, string currentLine)
        {
            var match = Regex.Match(currentLine, @"s!? ([a-zA-Z-_\.]+) (\w+) (\d+) (\d+) (\d+)");
            var name = match.Groups[1].Value;
            var type = match.Groups[2].Value;
            var num = int.Parse(match.Groups[3].Value);
            var interval = int.Parse(match.Groups[4].Value);
            var value = int.Parse(match.Groups[5].Value);

            Console.WriteLine("Sending {0} {1} {2} metrics every {3}ms with a value of {4}", name, num, type, interval, value);

            Action sendMetricsAction = () =>
            {
                var generator = Observable.Generate(
                    0,
                    x => x < num,
                    x => x + 1,
                    x => x,
                    x => TimeSpan.FromMilliseconds(interval)).Finally(() => Console.WriteLine("Finished sending stats: {0}", name));

                Action sendMetric;

                switch (type)
                {
                    case "count":
                        sendMetric = () => client.Count(name, value);
                        break;
                    case "gauge":
                        sendMetric = () => client.Gauge(name, value);
                        break;
                    default:
                        throw new FormatException("Unknown metric type: " + type);
                }

                generator.Subscribe(i => sendMetric());
            };

            if (currentLine.Contains("!"))
            {
                Task.Factory.StartNew(sendMetricsAction);
            }
            else
            {
                sendMetricsAction();
            }
        }
    }
}
