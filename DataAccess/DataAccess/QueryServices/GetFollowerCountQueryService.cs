using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Entities.Blog;
using Domain.Data.Queries;
using Domain.Business.QueryServices;
using DatabaseFactory.Data.Contracts;
using EnsureThat;

namespace DataAccess.DataAccess.QueryServices
{
    public class GetFollowerCountQueryService : IQueryService<GetFollowerCountQuery, int>
    {
        public GetFollowerCountQueryService(IDatabase database)
        {
            EnsureArg.IsNotNull(database, nameof(database));

            Database = database;
        }

        public IDatabase Database { get; }

        public int Execute(GetFollowerCountQuery query)
        {
            EnsureArg.IsNotNull(query, nameof(query));

            return Database.TryExecuteTransaction((transaction) =>
            {
                var dbQuery = Database.CreateStoredProcCommand("Blog.GetFollowerCount", transaction);
                var userId = Database.CreateParameter("UserId", query.UserId);

                dbQuery.Parameters.Add(userId);

                return Database.ExecuteScalar<int>(dbQuery);
            });
        }
    }
}
