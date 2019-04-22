using Domain.Entities.Blog;

namespace Domain.Data.Queries
{
    public class FetchAllUsersQuery : IPaginatedQuery<User>
    {
        public bool WithDeleted { get; set; }
        public PageInfo Paging { get; set; } = new PageInfo();
    }
}
