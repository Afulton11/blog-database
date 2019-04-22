using System.ComponentModel.DataAnnotations;


namespace Domain.Data.Commands
{
    class CreateUserCommand : ICommand
    {
        public int RoleId { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        public bool IsEmailVerified { get; set; }
    }
}
