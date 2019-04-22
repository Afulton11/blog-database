using Domain.Data;
namespace Domain.Entities.Blog
{
    public class PointReason : IEntity
    {
        public int ReasonId { get; set; }
        public string Reason { get; set; }
    }
}
