namespace Statr.Routing
{
    public class RouteDefinition
    {
        public RouteDefinition(string pattern)
        {
            Pattern = pattern;
        }

        public string Pattern { get; private set; }
    }
}