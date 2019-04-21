using Domain.Entities.Blog;
using System.ComponentModel.DataAnnotations;

namespace Domain.Data.Commands.Articles
{
    public class UpdateArticleCommand : ICommand
    {
        /// <summary>
        /// The <see cref="Article"/> to update
        /// </summary>
        [Required]
        public Article Article { get; set; }
    }
}
