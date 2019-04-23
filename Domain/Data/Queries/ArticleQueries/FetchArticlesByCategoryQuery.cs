using Domain.Entities.Blog;
using System.ComponentModel.DataAnnotations;

namespace Domain.Data.Queries.ArticleQueries
{
    public class FetchArticlesByCategoryQuery : IPaginatedQuery<Article>
    {
        /// <summary>
        /// The <see cref="ArticleCategory.Name"/> to fetch a paginated list of <see cref="Article"/>
        /// </summary>
        [Required]
        public string ArticleCategoryName { get; set; }
        public PageInfo Paging { get; set; } = new PageInfo();
    }
}
