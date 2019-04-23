using DataAccess.QueryServices;
using DataAccess.QueryServices.Readers;
using DatabaseFactory.Data.Contracts;
using Domain.Data.Queries;
using Domain.Entities.Blog;
using EnsureThat;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DataAccess.DataAccess.QueryServices.UserQueries
{
    public class FetchUserByIdQueryService : DbQueryService<FetchUserByIdQuery, User>
    {
        private readonly IReader<User> userReader;

        public FetchUserByIdQueryService(IDatabase database, IReader<User> userReader) : base(database)
        {
            EnsureArg.IsNotNull(userReader, nameof(userReader));
            this.userReader = userReader;
        }

        protected override User ReadQueryResult(IDataReader reader, FetchUserByIdQuery query) =>
            userReader.Read(reader).FirstOrDefault();

        protected override IEnumerable<IDataParameter> GetParameters(FetchUserByIdQuery query)
        {
            yield return Database.CreateParameter("UserId", query.UserId);
        }

        protected override string ProcedureName => "Blog.GetUserById";

    }
}
