using System.ComponentModel.DataAnnotations;

namespace Domain.Data.Commands
{
    /// <summary>
    /// Inserts an Author.
    /// </summary>
    public class CreateAuthorCommand : ICommand
    {
        [Required]
        public string AuthorUserID { get; set; }
    }
}