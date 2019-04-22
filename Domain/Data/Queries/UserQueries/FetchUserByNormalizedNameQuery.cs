using Domain.Entities.Blog;
using System.ComponentModel.DataAnnotations;

namespace Domain.Data.Queries.UserQueries
{
    public class FetchUserByNormalizedNameQuery : IQuery<User>
    {
        [Required]
        public string NormalizedUsername { get; set; }
    }
}
