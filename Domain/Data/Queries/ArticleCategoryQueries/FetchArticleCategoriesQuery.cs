using Domain.Entities.Blog;
using System.Collections.Generic;

namespace Domain.Data.Queries.ArticleCategoryQueries
{
    public class FetchArticleCategoriesQuery : IQuery<IEnumerable<ArticleCategory>>
    {
    }
}
