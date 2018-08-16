namespace Blog.Core
{
    public class UserInfo : IUserInfo
    {
        public string Domain { get; set; }

        public string Name { get; set; }

        public string[] Roles { get; set; }
    }
}
