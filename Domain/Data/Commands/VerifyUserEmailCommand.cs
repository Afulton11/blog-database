using System.ComponentModel.DataAnnotations;

namespace Domain.Data.Commands
{
    class VerifyUserEmailCommand : ICommand
    {
        [Required]
        public int UserId { get; set; }
    }
}
