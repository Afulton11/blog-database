using DataAccess.DataAccess.QueryServices;
using Domain.Entities.Blog;
using System.ComponentModel.DataAnnotations;

namespace Domain.Data.Queries
{
    /// <summary>
    /// A Command used to fetch all articles a user has favorited
    /// We say "fetch" because this query does not throw if no articles are found, rather it returns null.
    /// </summary>
    public class FetchUserFavoriteArticlesQuery : IQuery<Paged<Article>>
    {
        /// <summary>
        /// The user to retrieve a list of favorites for.
        /// </summary>
        [Required]
        public int UserId { get; set; }
        [Required]
        public PageInfo Paging { get; set; }
    }
}
