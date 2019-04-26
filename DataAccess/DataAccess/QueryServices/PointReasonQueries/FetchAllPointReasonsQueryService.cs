using DataAccess.QueryServices;
using DataAccess.QueryServices.Readers;
using DatabaseFactory.Data.Contracts;
using Domain.Data.Queries.PointReasonQueries;
using Domain.Entities.Blog;
using EnsureThat;
using System.Collections.Generic;
using System.Data;

namespace DataAccess.DataAccess.QueryServices.PointReasonQueries
{
    public class FetchAllPointReasonsQueryService : DbQueryService<FetchAllPointReasonQuery, IEnumerable<PointReason>>
    {
        private readonly IReader<PointReason> PointReasonReader;
        protected override string ProcedureName => "Blog.FetchAllPointReasons";

        public FetchAllPointReasonsQueryService
            (
                IDatabase database,
                IReader<PointReason> pointReasonReader
            ) : base(database)
        {
            EnsureArg.IsNotNull(pointReasonReader, nameof(pointReasonReader));
            PointReasonReader = pointReasonReader;
        }

        protected override IEnumerable<PointReason> ReadQueryResult
            (
                IDataReader reader,
                FetchAllPointReasonQuery query
            ) => PointReasonReader.Read(reader);

        protected override IEnumerable<IDataParameter> GetParameters(FetchAllPointReasonQuery query)
        {
            return new List<IDataParameter>();
        }
    }
}
