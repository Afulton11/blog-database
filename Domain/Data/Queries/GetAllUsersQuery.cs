using Domain.Entities.Blog;
using DatabaseFactory.Data.Contracts;
using System.ComponentModel.DataAnnotations;

namespace Domain.Data.Queries
{
    public class GetAllUsersQuery : IQuery<IEnumerable<User>>
    {
        public bool WithDeleted { get; set; }
    }
}
