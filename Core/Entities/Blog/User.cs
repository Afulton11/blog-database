using System;
using Core.Data;
namespace Core.Entities.Blog
{
    public class User : IEntity
    {
        public int UserId { get; set; }
        public Role Role { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public bool IsEmailVerified { get; set; }
        public DateTimeOffset CreatedOn { get; }
        public DateTimeOffset LastUpdatedOn { get; }
    }
}
