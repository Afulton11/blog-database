using System.ComponentModel.DataAnnotations;

namespace Domain.Data.Commands.Favorite
{
    public class CreateFavoriteArticleCommand : ICommand
    {
        /// <summary>
        /// The <see cref="Entities.Blog.User.UserId"/> that the favorite should be created for.
        /// </summary>
        [Required]
        public int UserId { get; set; }
        /// <summary>
        /// The <see cref="Entities.Blog.Article.ArticleId"/> that the favorite belongs to.
        /// </summary>
        [Required]
        public int ArticleId { get; set; }
    }
}
