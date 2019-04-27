using Domain.Entities.Blog;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Data.Queries.PointQueries
{
    public class FetchPointsByUserIdQuery : IQuery<IEnumerable<Point>>
    {
        [Required]
        public int UserId { get; set; }
    }
}
