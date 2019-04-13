using Core.Data;
using System;

namespace Core.Entities.Blog
{
    public class ArticleCategory : IEntity
    {
        public int ArticleCategoryID { get; set; }
        public string Name { get; set; }
        public int CreationUserID { get; set; }
        public DateTimeOffset CreationDateTime { get; set; }
        public DateTimeOffset LastUpdateDateTime { get; set; }
    }
}