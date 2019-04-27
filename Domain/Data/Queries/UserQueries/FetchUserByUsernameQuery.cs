using Domain.Entities.Blog;
using System.ComponentModel.DataAnnotations;

namespace Domain.Data.Queries
{
    public class FetchUserByUsernameQuery : IQuery<User>
    {
        [Required]
        public string Username { get; set; }
    }
}
