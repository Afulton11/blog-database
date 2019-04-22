using Common;
using Domain.Entities.Blog;
using System.Data;

namespace DataAccess.QueryServices.Readers
{
    public class ReasonReader : Reader<PointReason>
    {
        protected override PointReason ReadRow(IDataRecord row) =>
            new PointReason
            {
                ReasonId = row.GetSafely<int>(nameof(PointReason.ReasonId)),
                Reason = row.GetSafely<string>(nameof(PointReason.Reason)),
            };
    }
}
