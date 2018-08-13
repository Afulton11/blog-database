using System;
using System.Collections.Generic;

namespace Blog.Core.EntityLayer.Blog
{
    public class Article : IAuditableEntity, IConcurrentEntity
    {
        public Article()
        {
        }

        public Article(Int64? articleID)
        {
            ArticleID = articleID;
        }

        public Int64? ArticleID { get; set; }

        public String ArticleTitle { get; set; }

        public String ArticleDescription { get; set; }

        public String ArticleBody { get; set; }

        public String CreationUser { get; set; }

        public DateTime? CreationDateTime { get; set; }

        public String LastUpdateUser { get; set; }

        public DateTime? LastUpdateDateTime { get; set; }

        public Byte[] Timestamp { get; set; }

        // Foreign Key ID's
        public Int64? AuthorID { get; set; }

        public Int64? NextArticleID { get; set; }

        public Int64? PreviousArticleID { get; set; }

        public Int16? ContentStatusID { get; set; }

        // Foreign Keys
        public virtual Author AuthorFk { get; set; }

        public virtual Article NextArticleFk { get; set; }

        public virtual Article PreviousArticleFk { get; set; }

        public virtual ContentStatus ContentStatusFk { get; set; }


        public virtual ICollection<ArticleCategory> ArticleCategories { get; set; }
    }
}
