using System.ComponentModel.DataAnnotations;

namespace Domain.Data.Commands.Articles
{
    public class RemoveArticleCommand : ICommand
    {
        /// <summary>
        /// The <see cref="ArticleId"/> of the the <see cref="Entities.Blog.Article"/> to remove.
        /// </summary>
        [Required]
        public int ArticleId { get; set; }
    }
}
