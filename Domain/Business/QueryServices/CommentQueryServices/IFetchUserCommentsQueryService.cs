using Domain.Data.Queries;
using Domain.Data.Queries.CommentQueries;
using Domain.Entities.Blog;

namespace Domain.Business.QueryServices.CommentQueryServices
{
    public interface IFetchUserCommentsQueryService : IQueryService<FetchUserCommentsQuery, Paged<Comment>>
    {
    }
}
