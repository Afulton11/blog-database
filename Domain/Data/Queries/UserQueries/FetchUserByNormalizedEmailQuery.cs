using Domain.Entities.Blog;
using System.ComponentModel.DataAnnotations;

namespace Domain.Data.Queries.UserQueries
{
    public class FetchUserByNormalizedEmailQuery : IQuery<User>
    {
        [Required]
        public string NormalizedEmail { get; set; }
    }
}
