using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Entities.Blog;
namespace Domain.Data.Queries
{
    public class GetFollowersQuery : IQuery<IEnumerable<Follower>>
    {
        [Required]
        public int userId { get; set; }
    }
}
