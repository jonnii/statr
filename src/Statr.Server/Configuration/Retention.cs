namespace Statr.Server.Configuration
{
    public class Retention
    {
        public Retention() { }

        public Retention(int frequency, int history)
        {
            Frequency = frequency;
            History = history;
        }

        public int Frequency { get; set; }

        public int History { get; set; }
    }
}