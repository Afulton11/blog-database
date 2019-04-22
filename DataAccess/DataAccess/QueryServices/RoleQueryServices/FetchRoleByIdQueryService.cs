using DataAccess.QueryServices;
using DataAccess.QueryServices.Readers;
using DatabaseFactory.Data.Contracts;
using Domain.Data.Queries.RoleQueries;
using Domain.Entities.Blog;
using EnsureThat;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DataAccess.DataAccess.QueryServices.RoleQueryServices
{
    public class FetchRoleByIdQueryService : DbQueryService<FetchRoleByIdQuery, Role>
    {
        private readonly IReader<Role> roleReader;
        public FetchRoleByIdQueryService(IDatabase database, IReader<Role> roleReader) : base(database)
        {
            EnsureArg.IsNotNull(roleReader, nameof(roleReader));

            this.roleReader = roleReader;
        }

        protected override Role ReadQueryResult(IDataReader reader, FetchRoleByIdQuery query) =>
            roleReader.Read(reader).FirstOrDefault();

        protected override IEnumerable<IDataParameter> GetParameters(FetchRoleByIdQuery query)
        {
            yield return Database.CreateParameter("@RoleId", query.RoleId);
        }

        protected override string ProcedureName => "Blog.FetchRoleById";
    }
}
