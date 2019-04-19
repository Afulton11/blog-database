using System.ComponentModel.DataAnnotations;

namespace Domain.Data.Commands
{
    public class CreateCommentCommand : ICommand
    {
        [Required]
        public int UserID { get; set; }
        [Required]
        public int ArticleID { get; set; }
        [Required]
        [StringLength(2048, MinimumLength = 1, ErrorMessage = "Body should be minimum of 1 character")]
        public string Body { get; set; }
        public int? ParentCommentID { get; set; }
    }
}
