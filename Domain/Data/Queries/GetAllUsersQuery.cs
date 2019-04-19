using Domain.Entities.Blog;
using System.Collections.Generic;

namespace Domain.Data.Queries
{
    public class GetAllUsersQuery : IQuery<IEnumerable<User>>
    {
        public bool WithDeleted { get; set; }
    }
}
