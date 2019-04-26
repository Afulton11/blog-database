using DataAccess.QueryServices;
using DataAccess.QueryServices.Readers;
using DatabaseFactory.Data.Contracts;
using Domain.Data.Queries.PointQueries;
using Domain.Entities.Blog;
using EnsureThat;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DataAccess.DataAccess.QueryServices.PointQueries
{
    public class FetchPointsByUserIdQueryService : DbQueryService<FetchPointsByUserIdQuery, IEnumerable<Point>>
    {
        private readonly IReader<Point> PointReader;
        protected override string ProcedureName => "Blog.FetchPointsByUserId";

        public FetchPointsByUserIdQueryService
            (
                IDatabase database,
                IReader<Point> pointReader
            ) : base(database)
        {
            EnsureArg.IsNotNull(pointReader, nameof(pointReader));
            PointReader = pointReader;
        }

        protected override IEnumerable<Point> ReadQueryResult
            (
                IDataReader reader,
                FetchPointsByUserIdQuery query
            ) => PointReader.Read(reader);

        protected override IEnumerable<IDataParameter> GetParameters(FetchPointsByUserIdQuery query)
        {
            yield return Database.CreateParameter("UserId", query.UserId);
        }
    }
}
