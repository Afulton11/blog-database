using Domain.Data.Queries;

namespace Domain.Business
{
    public interface IQueryProcessor
    {
        TResult Execute<TResult>(IQuery<TResult> query);
    }
}
