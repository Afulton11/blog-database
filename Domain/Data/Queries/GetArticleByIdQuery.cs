using Domain.Entities.Blog;
using System.ComponentModel.DataAnnotations;

namespace Domain.Data.Queries
{
    public class GetArticleByIdQuery : IQuery<Article>
    {
        /// <summary>
        /// The Id of the <see cref="Article"/> to get.
        /// </summary>
        [Required]
        public int ArticleID { get; set; }
    }
}
