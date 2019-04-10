using DatabaseFactory.Data.Contracts;
using System.ComponentModel.DataAnnotations;

namespace Core.Data.Commands
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
