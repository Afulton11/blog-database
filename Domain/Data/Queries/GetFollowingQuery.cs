using System.ComponentModel.DataAnnotations;
using Domain.Entities.Blog;
namespace Domain.Data.Queries
{
    public class GetFollowingQuery : IPaginatedQuery<Follower>
    {
        [Required]
        public int UserId { get; set; }
        public PageInfo Paging { get; set; } = new PageInfo();
    }
}
