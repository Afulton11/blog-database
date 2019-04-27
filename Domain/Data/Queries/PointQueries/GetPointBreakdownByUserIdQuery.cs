using Domain.Entities.Blog;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Data.Queries.PointQueries
{
    public class GetPointBreakdownByUserIdQuery : IQuery<IEnumerable<PointBreakdown>>
    {
        [Required]
        public int UserId { get; set; }
    }
}
