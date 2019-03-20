using System.Collections.Generic;
using Blog.Core.EntityLayer.Blog;

namespace Blog.Core.BusinessLayer.Requests
{
    public interface ICreateArticleRequest : IRequest
    {
        string Title { get; set; }

        string Description { get; set; }

        string Body { get; set; }

        Author Author { get; set; }

        ContentStatus status { get; set; }

        IEnumerable<ArticleCategory> Category { get; set; }
    }
}
