using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.DataAccess.QueryServices
{
    public class GetFollowingQueryService : IQueryService<GetFollowingQuery, IEnumerable<Following>>
    {
        public GetFollowingQueryService(IDatabase database, IReader<Following> followingReader)
        {
            EnsureArg.IsNotNull(database, nameof(database));
            EnsureArg.IsNotNull(followerReader, nameof(followingReader));

            Database = database;
            FollowingReader = followerReader;
        }

        public IDatabase Database { get; }
        public IReader<Following> FollowingReader { get; }

        public IEnumerable<Following> Execute(GetFollowingQuery query)
        {
            EnsureArg.IsNotNull(query, nameof(query));

            return Database.TryExecuteTransaction((transaction) =>
            {
                var dbQuery = Database.CreateStoredProcCommand("Blog.GetFollowing", transaction);
                var userId = Database.CreateParameter("UserId", query.userId);

                dbQuery.Parameters.Add(userId);

                return Database.ExecuteReader(dbQuery, (reader) => this.GetFollowing(reader, query));
            });
        }

        private IEnumerable<Following> GetFollowing(IDataReader reader, GetFollowingQuery query)
        {
            var followers = FollowingReader.Read(reader);

            if (following == null || !following.Any())
            {
                throw new FollowingNotFoundException(query.userId);
            }
            return following;
        }
    }
}
