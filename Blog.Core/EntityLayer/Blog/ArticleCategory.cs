using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Blog.Core.EntityLayer.Blog
{
    public class ArticleCategory : IAuditableEntity, IConcurrentEntity
    {
        public ArticleCategory()
        {
        }

        public ArticleCategory(Int16? articleCategoryID)
        {
            ArticleCategoryID = articleCategoryID;
        }

        public Int16? ArticleCategoryID { get; set; }

        public String ArticleCategoryName { get; set; }


        public virtual ICollection<Article> Articles { get; set; } = new Collection<Article>();

        public byte[] Timestamp { get; set; }

        public string CreationUser { get; set; }

        public DateTime? CreationDateTime { get; set; }

        public string LastUpdateUser { get; set; }

        public DateTime? LastUpdateDateTime { get; set; }
    }
}
