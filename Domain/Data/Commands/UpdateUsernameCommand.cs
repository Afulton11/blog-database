using System.ComponentModel.DataAnnotations;

namespace Domain.Data.Commands
{
    class UpdateUsernameCommand : ICommand
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public string UpdatedUsername { get; set; }
    }
}
