using System.ComponentModel.DataAnnotations;

namespace Domain.Data.Queries.FavoriteQueries
{
    public class FetchArticleFavoriteCountQuery : IQuery<int>
    {
        [Required]
        public int ArticleId { get; set; }
    }
}
