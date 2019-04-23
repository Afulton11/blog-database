using Domain.Data;
using Domain.Entities.Blog;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.View
{
    public class PreviewArticle : IEntity
    {
        public Author Author { get; set; }
        public string CreatedDateString { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int ArticleId { get; set; }
    }
}
