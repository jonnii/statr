namespace Statr.Interactive
{
    public class GeneratorRequest
    {
        public GeneratorRequest(string name, string type, int num, Range interval, Range value)
        {
            Name = name;
            Type = type;
            Num = num;
            Interval = interval;
            Value = value;
        }

        public string Name { get; private set; }

        public string Type { get; private set; }

        public int Num { get; private set; }

        public Range Interval { get; private set; }

        public Range Value { get; private set; }

        public override string ToString()
        {
            return string.Format(
                "Sending {0} {1} {2} metrics every {3}ms with a value of {4}",
                Name,
                Num,
                Type,
                Interval,
                Value);
        }

        public int GetNextInterval()
        {
            return Interval.GetValue();
        }

        public float GetValue()
        {
            return Value.GetValue();
        }
    }
}