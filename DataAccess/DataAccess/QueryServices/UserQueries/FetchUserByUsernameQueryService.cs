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
    public class FetchUserByUsernameQueryService : DbQueryService<FetchUserByUsernameQuery, User>
    {
        private readonly IReader<User> userReader;

        public FetchUserByUsernameQueryService(IDatabase database, IReader<User> userReader) : base(database)
        {
            EnsureArg.IsNotNull(userReader, nameof(userReader));

            this.userReader = userReader;
        }

        protected override User ReadQueryResult(IDataReader reader, FetchUserByUsernameQuery query) =>
            userReader.Read(reader).FirstOrDefault();

        protected override IEnumerable<IDataParameter> GetParameters(FetchUserByUsernameQuery query)
        {
            yield return Database.CreateParameter("@Username", query.Username);
        }

        protected override string ProcedureName => "Blog.FetchUserByUsername";
    }
}
