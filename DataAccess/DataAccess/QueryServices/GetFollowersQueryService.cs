using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.DataAccess.QueryServices
{
    /// <summary>
    /// Retrieves the list of a user's followers
    /// </summary>
    public class GetFollowersQueryService : IQueryService<GetFollowersQuery, IEnumerable<Follower>> 
    {
        public GetFollowersQueryService(IDatabase database, int userId, IReader<Follower> followerReader)
        {
            EnsureArg.IsNotNull(database, nameof(database));
            EnsureArg.IsNotNull(userId, nameof(userId));
            EnsureArg.IsNotNull(followerReader, nameof(followerReader));

            Database = database;
            UserId = userId;
            FollowerReader = followerReader;
        }
        
    }

    public IDatabase Database { get; }
    public int UserId { get; }
    public IReader<Follower> FollowerReader { get; }

    public IEnumerable<Follower> Execute(GetFollowersQuery query)
    {
        EnsureArg.IsNotNull(query, nameof(query));

        return Database.TryExecuteTransaction((transaction) =>
        {
            var dbQuery = Database.CreateStoredProcCommand("Blog.GetFollowers", transaction);
            var userId = Database.CreateParameter("UserId", query.UserID);
            var followedUserId = Database.CreateParameter("FollowedUserId", query.followedUserId);

            dbQuery.Parameters.Add(userId);
            dbQuery.Parameters.Add(followedUserId);           

            return Database.ExecuteReader(dbQuery, (reader) => this.GetFollowers(reader, query));
        });
    }

    private IEnumerable<Follower> GetFollowers(IDataReader reader, GetFollowersQuery query)
    {
        var followers = FollowerReader.Read(reader);

        if (followers == null || !followers.Any())
        {
            throw new FollowersNotFoundException(query.UserId);
        }
        return followers;
    }
}
