using Core.Data;
using System;

namespace Core.Entities.Blog
{
    public class Article : IEntity
    {
        public int ArticleId { get; set; }
        public int AuthorId { get; set; }
        public int CategoryId { get; set; }
        public ContentStatus ContentStatus { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Body { get; set; }
        public DateTime CreationDateTime { get; set; }
        public DateTime LastUpdateDateTime { get; set; }
    }
}
