using DataAccess.QueryServices;
using DataAccess.QueryServices.Readers;
using DatabaseFactory.Data.Contracts;
using Domain.Data.Queries.PointReasonQueries;
using Domain.Entities.Blog;
using EnsureThat;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DataAccess.DataAccess.QueryServices.PointReasonQueries
{
    public class GetReasonForPointQueryService : DbQueryService<GetReasonForPointQuery, PointReason>
    {
        private readonly IReader<PointReason> PointReasonReader;
        protected override string ProcedureName => "Blog.GetReasonForPoint";

        public GetReasonForPointQueryService
            (
                IDatabase database,
                IReader<PointReason> pointReasonReader
            ) : base(database)
        {
            EnsureArg.IsNotNull(pointReasonReader, nameof(pointReasonReader));
            PointReasonReader = pointReasonReader;
        }

        protected override PointReason ReadQueryResult
            (
                IDataReader reader,
                GetReasonForPointQuery query
            ) => PointReasonReader.Read(reader).FirstOrDefault();

        protected override IEnumerable<IDataParameter> GetParameters(GetReasonForPointQuery query)
        {
            yield return Database.CreateParameter("PointId", query.PointId);
        }
    }
}
