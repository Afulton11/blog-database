﻿using System;

namespace Blog.Core.EntityLayer.Blog
{
    public sealed class ContentStatus
    {
        public ContentStatus()
        {
        }

        public ContentStatus(Int16? contentStatusID)
        {
            ContentStatusID = contentStatusID;
        }

        public Int16? ContentStatusID { get; set; }

        public String ContentStatusName { get; set; }
    }
}
