using Domain.Entities.Blog;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Data.Queries.FavoriteQueries
{
    public class FetchFavoriteArticlesByUserIdQuery : IQuery<IEnumerable<Article>>
    {
        [Required]
        public int UserId { get; set; }
    }
}
