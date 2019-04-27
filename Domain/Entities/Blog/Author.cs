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
        public DateTime BirthDate { get; set; }
        public DateTime DeletedAt { get; set; }
    }
}
