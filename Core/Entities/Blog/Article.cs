using Core.Data;
using System;

namespace Core.Entities.Blog
{
    public class Article : IEntity
    {
        public int ArticleID { get; set; }
        public int AuthorID { get; set; }
        public int CategoryID { get; set; }
        public ContentStatus ContentStatus { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Body { get; set; }
        public DateTimeOffset CreationDateTime { get; set; }
        public DateTimeOffset LastUpdateDateTime { get; set; }
    }
}
