using System.Collections.Generic;
using Blog.Core.EntityLayer.Blog;

namespace Blog.Core.BusinessLayer.Requests
{
    public sealed class CreateArticleRequest : ICreateArticleRequest
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Body { get; set; }

        public Author Author { get; set; }

        public ContentStatus status { get; set; }

        public IEnumerable<ArticleCategory> Category { get; set; }
    }
}
