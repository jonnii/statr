using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Statr.Server.Configuration
{
    public class RetentionParser
    {
        private readonly static Dictionary<string, Func<int, int>> timeSpanConverters =
            new Dictionary<string, Func<int, int>>();

        static RetentionParser()
        {
            timeSpanConverters.Add("s", i => i);
            timeSpanConverters.Add("m", i => i * 60);
            timeSpanConverters.Add("h", i => i * 3600);
            timeSpanConverters.Add("d", i => i * 86400);
            timeSpanConverters.Add("w", i => i * 604800);
            timeSpanConverters.Add("y", i => i * 31556926);
        }

        public static Retention Parse(string retention)
        {
            if (!retention.Contains(":"))
            {
                NotifyInvalidFormat(retention);
            }

            var bits = retention.Split(':');

            if (bits.Length != 2)
            {
                NotifyInvalidFormat(retention);
            }

            var rawFrequency = bits[0];
            var rawHistory = bits[1];

            var frequency = ConvertToSeconds(rawFrequency);
            var history = ConvertToSeconds(rawHistory);

            return new Retention(frequency, history);
        }

        private static int ConvertToSeconds(string rawTimeSpan)
        {
            var matches = Regex.Matches(rawTimeSpan, @"^(\d+)(.+)$");

            var rawOrdinal = matches[0].Groups[1].Value;
            var magnitude = matches[0].Groups[2].Value;

            var ordinal = int.Parse(rawOrdinal);

            if (!timeSpanConverters.ContainsKey(magnitude))
            {
                var message = string.Format(
                    "Could not parse time span: {0}",
                    rawTimeSpan);

                throw new FormatException(message);
            }

            return timeSpanConverters[magnitude](ordinal);
        }

        private static void NotifyInvalidFormat(string retention)
        {
            var message = string.Format(
                "Could not parse retention: {0}", retention);

            throw new FormatException(message);
        }
    }
}