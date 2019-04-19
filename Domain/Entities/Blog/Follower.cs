using System;
using Domain.Data;
namespace Domain.Entities.Blog
{
    public class Follower : IEntity
    {
        public int FollowedUserID { get; }
        public int FollowingUserID { get; }
        public DateTimeOffset AddedAt { get; }
        public DateTimeOffset DeletedAt { get; }
    }
}