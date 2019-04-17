using Core.Data.Queries;

namespace DatabaseFactory.Data.Contracts
{
    public interface IQueryProcessor<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        TResult process(TQuery query);
    }
}
