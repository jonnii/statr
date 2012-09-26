using Cassette;

namespace Statr.Web.Cassette
{
    public class CassetteConfiguration : IConfiguration<CassetteSettings>
    {
        public void Configure(CassetteSettings configurable)
        {
            configurable.IsDebuggingEnabled = false;
        }
    }
}