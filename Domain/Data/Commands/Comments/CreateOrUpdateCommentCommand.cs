using System.ComponentModel.DataAnnotations;

namespace Domain.Data.Commands.Comments
{
    public class CreateOrUpdateCommentCommand : ICommand
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int ArticleId { get; set; }
        [Required]
        [StringLength(2048, MinimumLength = 3, ErrorMessage = "Body should be minimum of 3 characters")]
        public string Body { get; set; }
        public int? CommentId { get; set; }
        public int? ParentCommentId { get; set; }
    }
}
