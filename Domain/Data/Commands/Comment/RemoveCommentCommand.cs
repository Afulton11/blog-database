using System.ComponentModel.DataAnnotations;

namespace Domain.Data.Commands.Comment
{
    public class RemoveCommentCommand : ICommand
    {
        /// <summary>
        /// The <see cref="Entities.Blog.Comment.CommentID"/> of the comment to remove
        /// </summary>
        [Required]
        public int CommentId { get; set; }
    }
}
