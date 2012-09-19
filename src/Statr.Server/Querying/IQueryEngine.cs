namespace Statr.Server.Querying
{
    public interface IQueryEngine
    {
        QueryResult Execute(Query query);
    }
}
