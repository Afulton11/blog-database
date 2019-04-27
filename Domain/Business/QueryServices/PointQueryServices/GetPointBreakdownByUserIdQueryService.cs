using Domain.Data.Queries;
using Domain.Data.Queries.PointReasonQueries;
using Domain.Data.Queries.PointQueries;
using Domain.Entities.Blog;
using Domain.Entities.View;
using EnsureThat;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Business.QueryServices.PointQueryServices
{
    public class GetPointBreakdownByUserIdQueryService : IQueryService<GetPointBreakdownByUserIdQuery, IEnumerable<PointBreakdown>>
    {
        private readonly IQueryProcessor QueryProcessor;

        public GetPointBreakdownByUserIdQueryService(IQueryProcessor queryProcessor)
        {
            EnsureArg.IsNotNull(queryProcessor, nameof(queryProcessor));
            QueryProcessor = queryProcessor;
        }

        public IEnumerable<PointBreakdown> Execute(GetPointBreakdownByUserIdQuery query)
        {
            var points = GetPoints(query.UserId);
            var pointReasons = GetPointReasons();

            int totalNumberOfPoints = points.Count();

            if (totalNumberOfPoints == 0)
            {
                return DefaultPointBreakdowns(pointReasons);
            }
            else
            {
                int totalPointsValue = points.Aggregate(0, (sum, nextPoint) => sum + nextPoint.Value);

                // avoid dividing by zero
                decimal valueDivisor = totalPointsValue > 0 ? totalPointsValue : 1;

                return CalculatePointBreakdowns(pointReasons, points, valueDivisor);
            }
        }

        private IEnumerable<PointBreakdown> DefaultPointBreakdowns(IEnumerable<PointReason> pointReasons)
        {
            foreach (PointReason pr in pointReasons)
            {
                yield return new PointBreakdown
                {
                    Reason = pr.Reason,
                    ValueTotal = 0,
                    ValuePercentage = 0,
                };
            }
        }

        private IEnumerable<PointBreakdown> CalculatePointBreakdowns
            (
                IEnumerable<PointReason> pointReasons,
                IEnumerable<Point> points,
                decimal valueDivisor
            )
        {
            foreach (PointReason pr in pointReasons)
            {
                int reasonId = pr.ReasonId;

                // total value from points for point reason
                int sumPointValueForReason = points.Aggregate(0, (sum, nextPoint) =>
                    nextPoint.ReasonId == reasonId ? sum + nextPoint.Value : sum);

                // percentage of point value for point reason
                decimal perPointValueForReason = decimal.Round((decimal)(100 * (sumPointValueForReason / valueDivisor)), 2, MidpointRounding.AwayFromZero);

                yield return new PointBreakdown
                {
                    Reason = pr.Reason,
                    ValueTotal = sumPointValueForReason,
                    ValuePercentage = perPointValueForReason,
                };
            }
        }

        private IEnumerable<Point> GetPoints(int userId)
        {
            return QueryProcessor.Execute(new FetchPointsByUserIdQuery
            {
                UserId = userId,
            });
        }

        private IEnumerable<PointReason> GetPointReasons()
        {
            return QueryProcessor.Execute(new FetchAllPointReasonQuery {});
        }
    }
}
