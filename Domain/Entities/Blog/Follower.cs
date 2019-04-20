using System;
using Domain.Data;
namespace Domain.Entities.Blog
{
    public class Follower : IEntity
    {
        public int FollowedUserID { get; }
        public int FollowingUserID { get; }
        public DateTime AddedAt { get; }
        public DateTime DeletedAt { get; }
    }
}