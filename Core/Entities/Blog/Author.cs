using System;
using Core.Data;
namespace Core.Entities.Blog
{
    public class Author : IEntity
    {
        public int AuthorUserID { get; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTimeOffset BirthDate { get; set; }
        public DateTimeOffset DeletedAt { get; }
    }
}
