using System;
using Domain.Data;
namespace Domain.Entities.Blog
{
    public class Author : IEntity
    {
        public int AuthorUserId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTimeOffset BirthDate { get; set; }
        public DateTimeOffset DeletedAt { get; set; }
    }
}
