using Domain.Entities.Blog;
using System.ComponentModel.DataAnnotations;

namespace Domain.Data.Queries.CommentQueries
{
    public class FetchArticleCommentsQuery : IQuery<Paged<Comment>>
    {
        /// <summary>
        /// The <see cref="Article.ArticleId"/> to fetch comments for.
        /// </summary>
        [Required]
        public int ArticleId { get; set; }
        public PageInfo Paging { get; set; } = new PageInfo();
    }
}
