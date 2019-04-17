using Core.Entities.Blog;
using DatabaseFactory.Data.Contracts;
using System.ComponentModel.DataAnnotations;

namespace Core.Data.Queries
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
