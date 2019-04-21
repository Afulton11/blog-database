using Common;
using Domain.Entities.Blog;
using System;
using System.Data;

namespace DataAccess.QueryServices.Readers
{
    public class PointReader : Reader<Point>
    {
        protected override Point ReadRow(IDataRecord row) =>
            new Point
            {
                PointId = row.GetSafely<int>(nameof(Point.PointId)),
                UserId = row.GetSafely<int>(nameof(Point.UserId)),
                ReasonId = row.GetSafely<int>(nameof(Point.ReasonId)),
                Value = row.GetSafely<int>(nameof(Point.Value)),
                CreatedAt = row.GetSafely<DateTime>(nameof(Point.CreatedAt)),
                ExpiresAt = row.GetSafely<DateTime>(nameof(Point.ExpiresAt)),
            };
    }
}
