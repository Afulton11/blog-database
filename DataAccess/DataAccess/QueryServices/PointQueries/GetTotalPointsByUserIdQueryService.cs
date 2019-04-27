using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Entities.Blog;
using Domain.Data.Queries.PointQueries;
using Domain.Business.QueryServices;
using DatabaseFactory.Data.Contracts;
using EnsureThat;

namespace DataAccess.DataAccess.QueryServices.PointQueries
{
    public class GetTotalPointsByUserIdQueryService : IQueryService<GetTotalPointsByUserIdQuery, int>
    {
        public IDatabase Database { get; }

        public GetTotalPointsByUserIdQueryService(IDatabase database)
        {
            EnsureArg.IsNotNull(database, nameof(database));

            Database = database;
        }

        public int Execute(GetTotalPointsByUserIdQuery query)
        {
            EnsureArg.IsNotNull(query, nameof(query));

            return Database.TryExecuteTransaction((transaction) =>
            {
                var dbQuery = Database.CreateStoredProcCommand("Blog.GetTotalPointsByUserId", transaction);

                dbQuery.Parameters.Add(Database.CreateParameter("UserId", query.UserId));

                return Database.ExecuteScalar<int>(dbQuery);
            });
        }
    }
}
