using Domain.Entities.Blog;

namespace Domain.Data.Queries
{
    public class GetAllUsersQuery : IPaginatedQuery<User>
    {
        public bool WithDeleted { get; set; }
        public PageInfo Paging { get; set; } = new PageInfo();
    }
}
