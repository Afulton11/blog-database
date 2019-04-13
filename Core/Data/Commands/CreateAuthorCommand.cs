using DatabaseFactory.Data.Contracts;
using System.ComponentModel.DataAnnotations;

namespace Core.Data.Commands
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