using DataAccess.QueryServices;
using DataAccess.QueryServices.Readers;
using DatabaseFactory.Data.Contracts;
using Domain.Data.Queries.UserQueries;
using Domain.Entities.Blog;
using EnsureThat;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DataAccess.QueryServices.UserQueries
{
    public class FetchUserByNormalizedEmailQueryService : DbQueryService<FetchUserByNormalizedEmailQuery, User>
    {
        private IReader<User> userReader;

        public FetchUserByNormalizedEmailQueryService(IDatabase database, IReader<User> userReader) : base(database)
        {
            EnsureArg.IsNotNull(userReader, nameof(userReader));
            this.userReader = userReader;
        }

        protected override User ReadQueryResult(IDataReader reader, FetchUserByNormalizedEmailQuery query) =>
            userReader.Read(reader).FirstOrDefault();

        protected override IEnumerable<IDataParameter> GetParameters(FetchUserByNormalizedEmailQuery query)
        {
            yield return Database.CreateParameter("@NormalizedEmail", query.NormalizedEmail);
        }

        protected override string ProcedureName => "Blog.FetchUserByNormalizedEmail";
    }
}

