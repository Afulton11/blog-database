using System.ComponentModel.DataAnnotations;

namespace Domain.Data.Commands
{
    /// <summary>
    /// Inserts a follower
    /// </summary>
    public class CreateFollowerCommand : ICommand
    {
        [Required]
        public int FollowedUserID { get; }
        public int FollowingUserID { get; }
    }
}
