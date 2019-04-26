using Common;
using Domain.Entities.Blog;
using System;
using System.Data;

namespace DataAccess.QueryServices.Readers
{
    public class PointBreakdownReader : Reader<PointBreakdown>
    {
        protected override PointBreakdown ReadRow(IDataRecord row) =>
            new PointBreakdown
            {
                Reason = row.GetSafely<string>(nameof(PointBreakdown.Reason)),
                ValueTotal = row.GetSafely<int>(nameof(PointBreakdown.ValueTotal)),
                ValuePercentage = row.GetSafely<decimal>(nameof(PointBreakdown.ValuePercentage)),
            };
    }
}
