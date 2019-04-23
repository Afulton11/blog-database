using System.Data;
using DataAccess.QueryServices.Readers;
using DatabaseFactory.Data.Contracts;
using Domain.Business.QueryServices;
using Domain.Data.Queries;
using Domain.Entities.Blog;
using EnsureThat;

namespace DataAccess.DataAccess.QueryServices
{
    /// <summary>
    /// Retrieves the list of a user's followers
    /// </summary>
    public class GetFollowersQueryService : IQueryService<GetFollowersQuery, Paged<Follower>>
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

        public Paged<Follower> Execute(GetFollowersQuery query)
        {
            EnsureArg.IsNotNull(query, nameof(query));

            return Database.TryExecuteTransaction((transaction) =>
            {
                var dbQuery = Database.CreateStoredProcCommand("Blog.GetFollowers", transaction);
                var userId = Database.CreateParameter("UserId", query.UserId);

                dbQuery.Parameters.Add(userId);

                return Database.ExecuteReader(dbQuery, (reader) => this.GetFollowers(reader, query));
            });
        }

        private Paged<Follower> GetFollowers(IDataReader reader, GetFollowersQuery query) =>
            new Paged<Follower>
            {
                Paging = query.Paging,
                Items = FollowerReader.Read(reader),
            };
    }
}
