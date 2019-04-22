using Common;
using Domain.Entities.Blog;
using System;
using System.Data;

namespace DataAccess.QueryServices.Readers
{
    public class UserReader : Reader<User>
    {
        protected override User ReadRow(IDataRecord row) =>
            new User
            {
                UserId = row.GetSafely<int>(nameof(User.UserId)),
                RoleId = row.GetSafely<int>(nameof(User.RoleId)),
                Username = row.GetSafely<string>(nameof(User.Username)),
                NormalizedUsername = row.GetSafely<string>(nameof(User.NormalizedUsername)),
                Password = row.GetSafely<string>(nameof(User.Password)),
                Email = row.GetSafely<string>(nameof(User.Email)),
                NormalizedEmail = row.GetSafely<string>(nameof(User.NormalizedEmail)),
                IsEmailVerified = row.GetSafely<bool>(nameof(User.IsEmailVerified)),
                CreationDateTime = row.GetSafely<DateTime>(nameof(User.CreationDateTime)),
                LastUpdatedDateTime = row.GetSafely<DateTime>(nameof(User.LastUpdatedDateTime)),
                DeletedAt = row.GetSafely<DateTime>(nameof(User.DeletedAt)),
            };
    }
}
