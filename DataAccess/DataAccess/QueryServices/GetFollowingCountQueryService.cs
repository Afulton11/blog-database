using DatabaseFactory.Data.Contracts;
using Domain.Business.QueryServices;
using Domain.Data.Queries;
using EnsureThat;

namespace DataAccess.DataAccess.QueryServices
{
    public class GetFollowingCountQueryService : IQueryService<GetFollowingCountQuery, int>
    {
        public GetFollowingCountQueryService(IDatabase database)
        {
            EnsureArg.IsNotNull(database, nameof(database));

            Database = database;
        }

        public IDatabase Database { get; }

        public int Execute(GetFollowingCountQuery query)
        {
            EnsureArg.IsNotNull(query, nameof(query));

            return Database.TryExecuteTransaction((transaction) =>
            {
                var dbQuery = Database.CreateStoredProcCommand("Blog.GetFollowingCount", transaction);
                var userId = Database.CreateParameter("UserId", query.UserId);

                dbQuery.Parameters.Add(userId);

                return Database.ExecuteScalar<int>(dbQuery);
            });
        }
    }
}
