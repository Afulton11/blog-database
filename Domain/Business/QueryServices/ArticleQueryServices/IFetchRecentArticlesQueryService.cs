using Domain.Data.Queries;
using Domain.Data.Queries.ArticleQueries;
using Domain.Entities.Blog;

namespace Domain.Business.QueryServices.ArticleQueryServices
{
    public interface IFetchRecentArticlesQueryService : IQueryService<FetchRecentArticlesQuery, Paged<Article>>
    {
    }
}
