using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Data.Commands
{
    /// <summary>
    /// Inserts an Author.
    /// </summary>
    public class CreateOrUpdateAuthorCommand : ICommand
    {
        [Required]
        public int UserId { get; set; }
        
        [Required]
        [MaxLength(64)]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }

        [Required]
        [MaxLength(64)]
        [DataType(DataType.Text)]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
    }
}