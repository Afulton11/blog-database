using System;
using Domain.Data;
namespace Domain.Entities.Blog
{
    public class Follower : IEntity
    {
        public int FollowedUserId { get; set; }
        public int UserId { get; set; }
        public DateTime AddedAt { get; set; }
        public DateTime DeletedAt { get; set; }
    }
}