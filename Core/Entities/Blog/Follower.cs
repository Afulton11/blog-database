using System;
using Core.Data;
namespace Core.Entities.Blog
{
    public class Follower : IEntity
    {
        public int FollowedUserID { get; }
        public int FollowingUserID { get; }
        public DateTimeOffset AddedAt { get; }
        public DateTimeOffset DeletedAt { get; }
    }
}