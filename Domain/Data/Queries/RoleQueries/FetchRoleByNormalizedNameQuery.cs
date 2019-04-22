using Domain.Entities.Blog;
using System.ComponentModel.DataAnnotations;

namespace Domain.Data.Queries.RoleQueries
{
    public class FetchRoleByNormalizedNameQuery : IQuery<Role>
    {
        [Required]
        public string NormalizedName { get; set; }
    }
}
