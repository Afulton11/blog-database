using Domain.Data.Commands;
using System.ComponentModel.DataAnnotations;

namespace Domain.Business.CommandServices.Favorite
{
    public class RemoveFavoriteArticleCommand : ICommand
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int ArticleId { get; set; }
    }
}