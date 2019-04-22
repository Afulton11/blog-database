using System;
using Domain.Data;
namespace Domain.Entities.Blog
{
    public class User : IEntity
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public bool IsEmailVerified { get; set; }
        public DateTime CreationDateTime { get; set; }
        public DateTime LastUpdatedDateTime { get; set; }
        public DateTime DeletedAt { get; set; }
    }
}
