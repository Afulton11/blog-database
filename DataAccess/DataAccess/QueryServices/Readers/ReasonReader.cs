using Common;
using Domain.Entities.Blog;
using System;
using System.Data;

namespace DataAccess.QueryServices.Readers
{
    public class ReasonReader : Reader<Reason>
    {
        protected override Reason ReadRow(IDataRecord row) =>
            new Reason
            {
                ReasonId = row.GetSafely<int>(nameof(Reason.ReasonId)),
                Reason = row.GetSafely<string>(nameof(Reason.Reason)),
            };
    }
}
