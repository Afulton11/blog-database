using Domain.Data.Queries;
using System.Threading.Tasks;

namespace Domain.Business
{
    public interface IAsyncQueryProcessor
    {
        Task<TResult> ExecuteAsync<TResult>(IQuery<TResult> query);
    }
}
