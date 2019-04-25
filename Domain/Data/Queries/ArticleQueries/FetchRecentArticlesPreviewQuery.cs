using Domain.Entities.View;

namespace Domain.Data.Queries.ArticleQueries
{
    public class FetchRecentArticlesPreviewQuery : IPaginatedQuery<PreviewArticle>
    {
        public PageInfo Paging { get; set; } = new PageInfo();
    }
}
