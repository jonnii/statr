namespace Statr.Server
{
    public interface IMetricParser
    {
        Metric Parse(string raw);
    }
}