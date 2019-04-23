using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.DataAccess.QueryServices
{
    public class GetFollowerCountQueryService : IQueryService<GetFollowerCountQuery, IEnumerable<Follower>>
    {
        public GetFollowersQueryService(IDatabase database, IReader<Follower> followerReader)
        {
            EnsureArg.IsNotNull(database, nameof(database));
            EnsureArg.IsNotNull(followerReader, nameof(followerReader));

            Database = database;
            FollowerReader = followerReader;
        }

        public IDatabase Database { get; }
        public IReader<Follower> FollowerReader { get; }

        public IEnumerable<Follower> Execute(GetFollowerCountQuery query)
        {
            EnsureArg.IsNotNull(query, nameof(query));

            return Database.TryExecuteTransaction((transaction) =>
            {
                var dbQuery = Database.CreateStoredProcCommand("Blog.GetFollowerCount", transaction);
                var userId = Database.CreateParameter("UserId", query.userId);

                dbQuery.Parameters.Add(userId);

                return Database.ExecuteScalar<int>(dbQuery);
            });
        }
    }
}
