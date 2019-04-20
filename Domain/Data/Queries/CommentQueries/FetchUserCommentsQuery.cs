using DataAccess.DataAccess.QueryServices;
using Domain.Entities.Blog;
using System.ComponentModel.DataAnnotations;

namespace Domain.Data.Queries.CommentQueries
{
    public class FetchUserCommentsQuery : IQuery<Paged<Comment>>
    {
        /// <summary>
        /// The <see cref="User.UserId"/> to fetch comments for.
        /// </summary>
        [Required]
        public int UserId { get; set; }
        public PageInfo Paging { get; set; } = new PageInfo();
    }
}
