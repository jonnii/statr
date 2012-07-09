using System;
using System.Reactive.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Statr.Client;

namespace Statr.Interactive
{
    public class MetricsGenerator : IMetricsGenerator
    {
        private readonly IStatrClient client;

        public MetricsGenerator(IStatrClient client)
        {
            this.client = client;
        }

        public Task SendMetrics(string line)
        {
            var request = BuildGeneratorRequest(line);

            return SendMetrics(request);
        }

        public Task SendMetrics(GeneratorRequest request)
        {
            var completionSource = new TaskCompletionSource<bool>();

            Action sendMetricsAction = () =>
            {
                var generator = Observable.Generate(
                    0,
                    x => x < request.Num,
                    x => x + 1,
                    x => x,
                    x => TimeSpan.FromMilliseconds(request.GetNextInterval()))
                    .Finally(() => completionSource.SetResult(true));

                Action sendMetric;

                switch (request.Type)
                {
                    case "count":
                        sendMetric = () => client.Count(request.Name, request.GetValue());
                        break;
                    case "gauge":
                        sendMetric = () => client.Gauge(request.Name, request.GetValue());
                        break;
                    default:
                        throw new FormatException("Unknown metric type: " + request.Type);
                }

                generator.Subscribe(i => sendMetric());
            };

            Task.Factory.StartNew(sendMetricsAction);

            return completionSource.Task;
        }

        public GeneratorRequest BuildGeneratorRequest(string line)
        {
            var match = Regex.Match(line, @"^([a-zA-Z-_\.]+) (\w+) (\d+) ([\d-]+) ([\d-]+)$");

            var name = match.Groups[1].Value;
            var type = match.Groups[2].Value;
            var num = int.Parse(match.Groups[3].Value);
            var interval = Range.Parse(match.Groups[4].Value);
            var value = Range.Parse(match.Groups[5].Value);

            return new GeneratorRequest(
                name, type, num, interval, value);
        }
    }
}