using Common;
using DataAccess.QueryServices.Readers;
using Domain.Entities.Blog;
using System;
using System.Data;

namespace DataAccess.DataAccess.QueryServices.Readers
{
    public class AuthorReader : Reader<Author>
    {
        protected override Author ReadRow(IDataRecord row) =>
            new Author
            {
                AuthorUserId = row.GetSafely<int>(nameof(Author.AuthorUserId)),
                FirstName = row.GetSafely<string>(nameof(Author.FirstName)),
                MiddleName = row.GetSafely<string>(nameof(Author.MiddleName)),
                LastName = row.GetSafely<string>(nameof(Author.LastName)),
                BirthDate = row.GetSafely<DateTime>(nameof(Author.BirthDate)),
                DeletedAt = row.GetSafely<DateTime>(nameof(Author.DeletedAt)),
            };
    }
}
