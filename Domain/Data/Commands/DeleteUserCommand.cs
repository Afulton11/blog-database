using System.ComponentModel.DataAnnotations;

namespace Domain.Data.Commands
{
    public class DeleteUserCommand : ICommand
    {
        [Required]
        public int UserId { get; set; }
    }
}
