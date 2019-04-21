using Common;
using Domain.Entities.Blog;
using System;
using System.Data;

namespace DataAccess.QueryServices.Readers
{
    public class RoleReader : Reader<Role>
    {
        protected override Role ReadRow(IDataRecord row) =>
            new Role
            {
                RoleId = row.GetSafely<int>(nameof(Role.RoleId)),
                Name = row.GetSafely<string>(nameof(Role.Name)),
            };
    }
}
