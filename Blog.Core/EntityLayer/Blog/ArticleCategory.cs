using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Blog.Core.EntityLayer.Blog
{
    public class ArticleCategory
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
    }
}
