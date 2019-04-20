using DataAccess.DataAccess.QueryServices;
using Domain.Entities.Blog;

namespace Domain.Data.Queries.ArticleQueries
{
    public class FetchRecentArticlesQuery : IQuery<Paged<Article>>
    {
        public PageInfo Paging { get; set; } = new PageInfo();
    }
}
