using Domain.Data;
using System;

namespace Domain.Entities.Blog
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