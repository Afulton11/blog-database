using Domain.Entities.Blog;
using System.ComponentModel.DataAnnotations;

namespace Domain.Data.Queries.ArticleQueries
{
    public class FetchArticlesByCategoryQuery : IPaginatedQuery<Article>
    {
        /// <summary>
        /// The <see cref="ArticleCategory.ArticleCategoryID"/> to fetch a paginated list of <see cref="Article"/>
        /// </summary>
        [Required]
        public int ArticleCategoryId { get; set; }
        public PageInfo Paging { get; set; } = new PageInfo();
    }
}
