using System.ComponentModel.DataAnnotations;

namespace Domain.Data.Commands.Roles
{
    public class DeleteRoleCommand : ICommand
    {
        [Required]
        public int RoleId { get; set; }
    }
}
