
namespace Domain.Data.Queries
{
    public interface IPaginatedQuery<TResultItem> : IQuery<Paged<TResultItem>>
    {
        PageInfo Paging { get; set; }
    }
}
