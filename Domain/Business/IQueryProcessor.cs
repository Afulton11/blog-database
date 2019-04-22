using Domain.Data.Queries;
using System.Threading.Tasks;

namespace Domain.Business
{
    public interface IQueryProcessor
    {
        Task<TResult> Execute<TResult>(IQuery<TResult> query);
    }
}
