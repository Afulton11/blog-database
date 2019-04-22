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
    public class GetUserByIdQueryService : DbQueryService<GetUserByIdQuery, User>
    {
        private readonly IReader<User> userReader;

        public GetUserByIdQueryService(IDatabase database, IReader<User> userReader) : base(database)
        {
            EnsureArg.IsNotNull(userReader, nameof(userReader));
            this.userReader = userReader;
        }

        protected override User ReadQueryResult(IDataReader reader, GetUserByIdQuery query)
        {
            var user = userReader.Read(reader)?.FirstOrDefault() ?? null;

            if (user == null)
            {
                throw new KeyNotFoundException($"No User with Id[{query.UserId}] was found in the database!");
            }

            return user;
        }

        protected override IEnumerable<IDataParameter> GetParameters(GetUserByIdQuery query)
        {
            yield return Database.CreateParameter("UserId", query.UserId);
        }

        protected override string ProcedureName => "Blog.GetUserById";

    }
}
