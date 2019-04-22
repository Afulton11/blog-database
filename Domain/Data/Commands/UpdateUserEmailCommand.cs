using System.ComponentModel.DataAnnotations;

namespace Domain.Data.Commands
{
    class UpdateUserEmailCommand : ICommand
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public string UpdatedEmail { get; set; }
    }
}
