﻿namespace Blog.Core
{
    public interface IUserInfo
    {
        string Domain { get; set; }

        string Name { get; set; }

        string[] Roles { get; set; }
    }
}
