using System.ComponentModel.DataAnnotations;

namespace Domain.Data.Commands
{
    class DeleteUserCommand : ICommand
    {
        [Required]
        public int UserId { get; set; }
    }
}
