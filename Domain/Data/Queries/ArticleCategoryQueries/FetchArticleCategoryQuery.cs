using Domain.Entities.Blog;
using System.ComponentModel.DataAnnotations;

namespace Domain.Data.Queries.ArticleCategoryQueries
{
    public class FetchArticleCategoryQuery : IQuery<ArticleCategory>
    {
        /// <summary>
        /// the <see cref="ArticleCategoryId"/> of the <see cref="Entities.Blog.ArticleCategory"/> to fetch.
        /// </summary>
        [Required]
        public int ArticleCategoryId { get; set; }
    }
}
