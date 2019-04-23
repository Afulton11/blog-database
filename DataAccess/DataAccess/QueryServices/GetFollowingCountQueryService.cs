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
                var userId = Database.CreateParameter("UserId", query.userId);

                dbQuery.Parameters.Add(userId);

                return Database.ExecuteScalar<int>(dbQuery);
            });
        }
    }
}
