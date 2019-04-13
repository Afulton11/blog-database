using Core.Data.Queries;

namespace Core.Business.Contracts
{
    public interface IQueryService<TQuery, TResult> where TQuery: IQuery<TResult>
    {
        TResult Execute(TQuery query);
    }
}
