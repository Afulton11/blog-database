using System;
namespace DatabaseFactory.Data.Contracts
{
    public interface IQueryProcessor
    {
        TResult process<TResult>(IQuery<TResult> query);
    }
}
