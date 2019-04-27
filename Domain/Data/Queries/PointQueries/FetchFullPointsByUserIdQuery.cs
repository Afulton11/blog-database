using Domain.Entities.View;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Data.Queries.PointQueries
{
    public class FetchFullPointsByUserIdQuery : IQuery<IEnumerable<FullPoint>>
    {
        [Required]
        public int UserId { get; set; }
    }
}
