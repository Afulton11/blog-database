using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.DataAccess.QueryServices
{
    public class GetFollowingCountQueryService : IQueryService<GetFollowingCountQuery, IEnumerable<Following>>
    {
        public GetFollowingCountQueryService(IDatabase database, IReader<Following> followingReader)
        {
            EnsureArg.IsNotNull(database, nameof(database));
            EnsureArg.IsNotNull(followerReader, nameof(followingReader));

            Database = database;
            FollowingReader = followerReader;
        }

        public IDatabase Database { get; }
        public IReader<Following> FollowingReader { get; }

        public IEnumerable<Following> Execute(GetFollowingCountQuery query)
        {
            EnsureArg.IsNotNull(query, nameof(query));

            return Database.TryExecuteTransaction((transaction) =>
            {
                var dbQuery = Database.CreateStoredProcCommand("Blog.GetFollowingCount", transaction);
                var userId = Database.CreateParameter("UserId", query.userId);

                dbQuery.Parameters.Add(userId);

                return Database.ExecuteScalar<int>(dbQuery);
            });
        }
    }
}
