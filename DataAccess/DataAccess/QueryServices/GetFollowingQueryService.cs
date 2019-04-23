using System.Collections.Generic;
using System.Data;
using DataAccess.QueryServices.Readers;
using DatabaseFactory.Data.Contracts;
using Domain.Business.QueryServices;
using Domain.Data.Queries;
using Domain.Entities.Blog;
using EnsureThat;

namespace DataAccess.DataAccess.QueryServices
{
    public class GetFollowingQueryService : IQueryService<GetFollowingQuery, Paged<Follower>>
    {
        public GetFollowingQueryService(IDatabase database, IReader<Follower> followingReader)
        {
            EnsureArg.IsNotNull(database, nameof(database));
            EnsureArg.IsNotNull(followingReader, nameof(followingReader));

            Database = database;
            FollowingReader = followingReader;
        }

        public IDatabase Database { get; }
        public IReader<Follower> FollowingReader { get; }

        public Paged<Follower> Execute(GetFollowingQuery query)
        {
            EnsureArg.IsNotNull(query, nameof(query));

            return Database.TryExecuteTransaction((transaction) =>
            {
                var dbQuery = Database.CreateStoredProcCommand("Blog.GetFollowing", transaction);
                var userId = Database.CreateParameter("UserId", query.UserId);

                dbQuery.Parameters.Add(userId);

                return Database.ExecuteReader(dbQuery, (reader) => this.GetFollowing(reader, query));
            });
        }

        private Paged<Follower> GetFollowing(IDataReader reader, GetFollowingQuery query) =>
            new Paged<Follower>
            {
                Paging = query.Paging,
                Items = FollowingReader.Read(reader),
            };
    }
}
