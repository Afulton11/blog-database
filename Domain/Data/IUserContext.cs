using Domain.Entities.Blog;

namespace Domain.Data
{
    public interface IUserContext
    {
        int? CurrentUserId { get; }
        bool IsInRole(Role role);
    }
}
