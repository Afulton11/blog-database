using System;
namespace Domain.Business.QueryServices.Exceptions
{
    public class FollowersNotFoundException : Exception
    {
        public FollowersNotFoundException(int authorId)
            : base(
$"No followers were found for the user with Id ${authorId}"
                  )
        {
        }
    }
}
