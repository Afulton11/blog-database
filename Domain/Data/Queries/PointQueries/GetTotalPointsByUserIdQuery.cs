using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Data.Queries.PointQueries
{
    public class GetTotalPointsByUserIdQuery : IQuery<int>
    {
        [Required]
        public int UserId { get; set; }
    }
}
