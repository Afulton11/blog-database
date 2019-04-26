using System.ComponentModel.DataAnnotations;

namespace Domain.Data.Queries.FavoriteQueries
{
    public class IsArticleFavoritedByUserQuery : IQuery<bool>
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int ArticleId { get; set; }
    }
}
