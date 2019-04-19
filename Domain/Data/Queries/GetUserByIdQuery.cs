using Domain.Entities.Blog;
using DatabaseFactory.Data.Contracts;
using System.ComponentModel.DataAnnotations;

namespace Domain.Data.Queries
{
    class GetUserByIdQuery : IQuery<User>
    {
        [Required]
        public int UserId { get; set; }
        public bool WithDeleted { get; set; }
    }
}
