using System.ComponentModel.DataAnnotations;

namespace Domain.Data.Commands.ArticleCategories
{
    public class CreateArticleCategoryCommand : ICommand
    {
        /// <summary>
        /// The <see cref="Name"/> of the <see cref="Entities.Blog.ArticleCategory"/> to create.
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// The <see cref="UserId"/> of the <see cref="Entities.Blog.User"/> creating the article category.
        /// </summary>
        [Required]
        public int CreationUserId { get; set; }
    }
}
