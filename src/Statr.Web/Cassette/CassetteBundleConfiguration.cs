using Cassette;
using Cassette.Scripts;
using Cassette.Stylesheets;

namespace Statr.Web.Cassette
{
    public class CassetteBundleConfiguration : IConfiguration<BundleCollection>
    {
        public void Configure(BundleCollection bundles)
        {
            bundles.AddPerIndividualFile<StylesheetBundle>("Content");
            bundles.Add<ScriptBundle>("Scripts/app");
        }
    }
}