using Domain.Entities.Blog;
using System.ComponentModel.DataAnnotations;

namespace Domain.Data.Queries.PointReasonQueries
{
    public class GetReasonForPointQuery : IQuery<PointReason>
    {
        [Required]
        public int PointId { get; set; }
    }
}
