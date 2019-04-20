using Common;
using DataAccess.QueryServices.Readers;
using Domain.Entities.Blog;
using System;
using System.Data;

namespace DataAccess.DataAccess.QueryServices.Readers
{
    public class FollowerReader : Reader<Follower>
    {
        protected override Follower ReadRow(IDataRecord row) =>
            new Follower
            {
                UserId = row.GetSafely<int>(nameof(Follower.UserId)),
                AddedAt = row.GetSafely<DateTime>(nameof(Follower.AddedAt)),
                DeletedAt = row.GetSafely<DateTime>(nameof(Follower.DeletedAt)),
            };
    }
}
