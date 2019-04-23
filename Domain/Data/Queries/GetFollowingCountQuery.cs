using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Entities.Blog;
namespace Domain.Data.Queries
{
    public class GetFollowingCountQuery : IQuery<int>
    {
        [Required]
        public int UserId { get; set; }
    }
}
