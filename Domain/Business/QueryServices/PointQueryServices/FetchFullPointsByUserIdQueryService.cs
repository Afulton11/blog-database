using Domain.Data.Queries;
using Domain.Data.Queries.PointReasonQueries;
using Domain.Data.Queries.PointQueries;
using Domain.Entities.Blog;
using Domain.Entities.View;
using EnsureThat;
using System;
using System.Collections.Generic;

namespace Domain.Business.QueryServices.PointQueryServices
{
    public class FetchFullPointsByUserId : IQueryService<FetchFullPointsByUserIdQuery, IEnumerable<FullPoint>>
    {
        private readonly IQueryProcessor QueryProcessor;

        public FetchFullPointsByUserId(IQueryProcessor queryProcessor)
        {
            EnsureArg.IsNotNull(queryProcessor, nameof(queryProcessor));
            QueryProcessor = queryProcessor;
        }

        public IEnumerable<FullPoint> Execute(FetchFullPointsByUserIdQuery query)
        {
            var points = QueryProcessor.Execute(new FetchPointsByUserIdQuery
            {
                UserId = query.UserId,
            });

            return GetFullPoints(points);
        }

        private IEnumerable<FullPoint> GetFullPoints(IEnumerable<Point> points)
        {
            foreach (Point p in points)
            {
                PointReason pointReason = QueryProcessor.Execute(new GetReasonForPointQuery
                {
                    PointId = p.PointId,
                });

                yield return new FullPoint
                {
                    PointId = p.PointId,
                    UserId = p.UserId,
                    Reason = pointReason.Reason,
                    Value = p.Value,
                    CreatedAt = p.CreatedAt,
                    ExpiresAt = p.ExpiresAt,
                };
            }
        }
    }
}
