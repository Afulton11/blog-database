using Domain.Entities.Blog;
using System.ComponentModel.DataAnnotations;

namespace Domain.Data.Commands.ArticleCategories
{
    public class UpdateArticleCategoryCommand : ICommand
    {
        /// <summary>
        /// The <see cref="ArticleCategory"/> to update.
        /// </summary>
        [Required]
        public ArticleCategory ArticleCategory { get; set; }
    }
}
