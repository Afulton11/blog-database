using System;
namespace DatabaseFactory.Data.Contracts
{
    public interface IQueryService<TQuery, TResult> where TQuery: IQuery<TResult>
    {
        TResult Execute(TQuery query);
    }
}
