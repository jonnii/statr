namespace Statr.Configuration
{
    public class Retention
    {
        public Retention() { }

        public Retention(string frequency, string history)
        {
            Frequency = frequency;
            History = history;
        }

        public string Frequency { get; set; }

        public string History { get; set; }
    }
}