using Domain.Data.Queries;
using Domain.Entities.Blog;
using DatabaseFactory.Data.Contracts;
using EnsureThat;
using System.Collections.Generic;
using System.Data;
using DataAccess.QueryServices.Readers;
using System;

namespace DataAccess.DataAccess.QueryServices.UserQueries
{
    public class FetchAllUsersQueryService : DbPagedQueryService<FetchAllUsersQuery, User>
    {
        public readonly IReader<User> userReader;

        public FetchAllUsersQueryService(IDatabase database, IReader<User> userReader) : base(database)
        {
            EnsureArg.IsNotNull(userReader, nameof(userReader));

            this.userReader = userReader;
        }

        protected override IEnumerable<User> ReadItems(IDataReader dataReader) =>
            userReader.Read(dataReader);

        protected override IEnumerable<IDataParameter> GetQueryParameters(FetchAllUsersQuery query)
        {
            yield return Database.CreateParameter("@WithDeleted", Convert.ToInt32(query.WithDeleted));
        }

        protected override string ProcedureName => "Blog.GetAllUsers";
    }
}
