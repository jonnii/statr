using System;
using System.Reactive.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Statr.Client;

namespace Statr.Interactive
{
    public class MetricsGenerator
    {
        public void Go()
        {
            ShowWelcomeBanner();

            using (var client = StatrClient.Build("127.0.0.1"))
            {
                Console.WriteLine("!!! Created client {0}", client);

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

        private void ShowInstructions()
        {
            Console.WriteLine("Instructions");
            Console.WriteLine("------------");
            Console.WriteLine();
            Console.WriteLine("send metrics \t\t s <name> <type> <num> <interval-range> <value-range>");
            Console.WriteLine("q - quit");
            Console.WriteLine();
            Console.WriteLine();
        }

        private void ShowWelcomeBanner()
        {
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("| Welcome to the Statr Interactive Console |");
            Console.WriteLine("--------------------------------------------");

            Console.WriteLine();
            Console.WriteLine();
        }

        private void SendMetrics(IStatrClient client, string currentLine)
        {
            var request = BuildGeneratorRequest(currentLine);

            Console.WriteLine(request);

            Action sendMetricsAction = () =>
            {
                var generator = Observable.Generate(
                    0,
                    x => x < request.Num,
                    x => x + 1,
                    x => x,
                    x => TimeSpan.FromMilliseconds(request.Interval))
                    .Finally(() => Console.WriteLine("Finished sending stats: {0}", request.Name));

                Action sendMetric;

                switch (request.Type)
                {
                    case "count":
                        sendMetric = () => client.Count(request.Name, request.Value);
                        break;
                    case "gauge":
                        sendMetric = () => client.Gauge(request.Name, request.Value);
                        break;
                    default:
                        throw new FormatException("Unknown metric type: " + request.Type);
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

        public GeneratorRequest BuildGeneratorRequest(string line)
        {
            var match = Regex.Match(line, @"s ([a-zA-Z-_\.]+) (\w+) (\d+) (\d+) (\d+)");

            var name = match.Groups[1].Value;
            var type = match.Groups[2].Value;
            var num = int.Parse(match.Groups[3].Value);
            var interval = int.Parse(match.Groups[4].Value);
            var value = int.Parse(match.Groups[5].Value);

            return new GeneratorRequest(
                name, type, num, interval, value);
        }
    }
}