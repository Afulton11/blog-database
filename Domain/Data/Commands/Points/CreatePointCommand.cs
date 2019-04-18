using System.ComponentModel.DataAnnotations;

namespace Domain.Data.Commands.Points
{
    /// <summary>
    /// Creates a new Point.
    /// </summary>
    public class CreatePointCommand : ICommand
    {
        /// <summary>
        /// The <see cref="Entities.Blog.User.UserId"/> that should receive the points
        /// </summary>
        [Required]
        public int UserId { get; set; }
    }
}
