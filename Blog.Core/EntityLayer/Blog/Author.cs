﻿using System;
using System.Collections.Generic;

namespace Blog.Core.EntityLayer.Blog
{
    public class Author: IAuditableEntity, IConcurrentEntity
    {
        public Author()
        {
        }

        public Author(Int64? authorID)
        {
            AuthorID = authorID;
        }

        public Int64? AuthorID { get; set; }

        public String FirstName { get; set; }

        public String MiddleName { get; set; }

        public String LastName { get; set; }

        public DateTime? BirthDate { get; set; }

        public string CreationUser { get; set; }

        public DateTime? CreationDateTime { get; set; }

        public string LastUpdateUser { get; set; }

        public DateTime? LastUpdateDateTime { get; set; }

        public byte[] Timestamp { get; set; }

        public virtual ICollection<Article> Articles { get; set; }
    }
}
