using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DataAccess.QueryServices.Readers;
using DatabaseFactory.Data.Contracts;
using Domain.Business.QueryServices;
using Domain.Business.QueryServices.Exceptions;
using Domain.Data.Queries;
using Domain.Entities.Blog;
using EnsureThat;

namespace DataAccess.DataAccess.QueryServices
{
    /// <summary>
    /// Retrieves the list of a user's followers
    /// </summary>
    public class GetFollowersQueryService : IQueryService<GetFollowersQuery, IEnumerable<Follower>>
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

        public IEnumerable<Follower> Execute(GetFollowersQuery query)
        {
            EnsureArg.IsNotNull(query, nameof(query));

            return Database.TryExecuteTransaction((transaction) =>
            {
                var dbQuery = Database.CreateStoredProcCommand("Blog.GetFollowers", transaction);
                var userId = Database.CreateParameter("UserId", query.userId);

                dbQuery.Parameters.Add(userId);

                return Database.ExecuteReader(dbQuery, (reader) => this.GetFollowers(reader, query));
            });
        }

        private IEnumerable<Follower> GetFollowers(IDataReader reader, GetFollowersQuery query)
        {
            var followers = FollowerReader.Read(reader);

            if (followers == null || !followers.Any())
            {
                throw new FollowersNotFoundException(query.userId);
            }
            return followers;
        }
    }
}
