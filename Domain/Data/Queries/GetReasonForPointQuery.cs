using Domain.Entities.Blog;
using DatabaseFactory.Data.Contracts;
using System.ComponentModel.DataAnnotations;

namespace Domain.Data.Queries
{
    class GetReasonForPointQuery : IQuery<PointReason>
    {
        [Required]
        public int PointId { get; set; }
    }
}
