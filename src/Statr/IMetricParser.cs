namespace Statr
{
    public interface IMetricParser
    {
        Metric Parse(string raw);
    }
}