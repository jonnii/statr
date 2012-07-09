using System.Threading.Tasks;

namespace Statr.Interactive
{
    public interface IMetricsGenerator
    {
        Task SendMetrics(string line);

        Task SendMetrics(GeneratorRequest request);
    }
}