using Domain.Entities.Blog;
using Domain.Entities.View;
using System.ComponentModel.DataAnnotations;

namespace Domain.Data.Queries.CommentQueries
{
    public class FetchArticleCommentsQuery : IPaginatedQuery<ArticleComment>
    {
        /// <summary>
        /// The <see cref="Article.ArticleId"/> to fetch comments for.
        /// </summary>
        [Required]
        public int ArticleId { get; set; }

        /// <summary>
        /// The maximum depth to fetch all replies for each root comment
        /// </summary>
        public int MaxDepth { get; set; } = 3;
        public PageInfo Paging { get; set; } = new PageInfo();
    }
}
