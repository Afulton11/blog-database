using Domain.Entities.Blog;
using System.ComponentModel.DataAnnotations;

namespace Domain.Data.Queries.RoleQueries
{
    public class FetchRoleByIdQuery : IQuery<Role>
    {
        [Required]
        public int RoleId { get; set; }
    }
}
