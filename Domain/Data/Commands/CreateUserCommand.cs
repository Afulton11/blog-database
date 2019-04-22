using System.ComponentModel.DataAnnotations;


namespace Domain.Data.Commands
{
    public class CreateUserCommand : ICommand
    {
        public int? RoleId { get; set; }
        [Required]
        public string Username { get; set; }
        public string NormalizedUsername { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string NormalizedEmail { get; set; }
        public bool IsEmailVerified { get; set; }
    }
}
