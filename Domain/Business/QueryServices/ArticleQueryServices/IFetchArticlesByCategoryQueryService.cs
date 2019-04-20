using Domain.Data.Queries;
using Domain.Data.Queries.ArticleQueries;
using Domain.Entities.Blog;

namespace Domain.Business.QueryServices.ArticleQueryServices
{
    public interface IFetchArticlesByCategoryQueryService : IQueryService<FetchArticlesByCategoryQuery, Paged<Article>>
    {
    }
}
