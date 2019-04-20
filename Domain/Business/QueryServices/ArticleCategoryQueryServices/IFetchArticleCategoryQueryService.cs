using Domain.Data.Queries.ArticleCategoryQueries;
using Domain.Entities.Blog;

namespace Domain.Business.QueryServices.ArticleCategoryQueryServices
{
    public interface IFetchArticleCategoryQueryService : IQueryService<FetchArticleCategoryQuery, ArticleCategory>
    {
    }
}
