using Domain.Data;
namespace Domain.Entities.Blog
{
    public class Role : IEntity
    {
        public int RoleId { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }
    }
}
