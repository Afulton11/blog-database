using Domain.Data.Queries;
using Domain.Entities.Blog;
using System.Collections.Generic;

namespace Domain.Business.QueryServices.ArticleQueryServices
{
    /// <summary>
    /// Fetches all the articles that a user has favorited.
    /// We say "fetch" because this query does not throw if no articles are found, rather it returns null.
    /// </summary>
    public interface IFetchUserFavoriteArticleQueryService : IQueryService<FetchUserFavoriteArticlesQuery, Paged<Article>>
    {
    }
}
