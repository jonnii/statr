using System;
using System.Globalization;

namespace Statr.Interactive
{
    public class Range
    {
        public static Range Parse(string value)
        {
            if (!value.Contains("-"))
            {
                var intValue = int.Parse(value);
                return new Range(intValue, intValue);
            }

            var bits = value.Split('-');
            var from = int.Parse(bits[0]);
            var to = int.Parse(bits[1]);

            if (from > to)
            {
                throw new FormatException("From cannot be greater than to for a range");
            }

            return new Range(from, to);
        }

        private readonly Random random;

        public Range(int from, int to)
        {
            From = from;
            To = to;
            random = new Random();
        }

        public int From { get; set; }

        public int To { get; set; }

        public int GetValue()
        {
            return random.Next(From, To);
        }

        public override string ToString()
        {
            if (From == To)
            {
                return From.ToString(CultureInfo.InvariantCulture);
            }

            return string.Format("{0}-{1}", From, To);
        }
    }
}