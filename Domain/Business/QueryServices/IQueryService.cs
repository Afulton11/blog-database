using Domain.Data.Queries;

namespace Domain.Business.QueryServices
{
    public interface IQueryService<TQuery, TResult> where TQuery: IQuery<TResult>
    {
        TResult Execute(TQuery query);
    }
}
