using System.ComponentModel.DataAnnotations;
namespace Domain.Data.Queries
{
    public class GetFollowerCountQuery : IQuery<int>
    {
        [Required]
        public int UserId { get; set; }
    }
}
