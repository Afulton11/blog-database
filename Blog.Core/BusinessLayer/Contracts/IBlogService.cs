using System;
using System.Threading.Tasks;
using Blog.Core.BusinessLayer.Responses;
using Blog.Core.EntityLayer.Blog;

namespace Blog.Core.BusinessLayer.Contracts
{
    public interface IBlogService : IService
    {
        Task<IPagedResponse<Author>> GetAuthorsAsync(int pageSize = 10, int pageNumber = 1);

        Task<ISingleResponse<Author>> GetAuthorAsync(Int64 id);

        Task<IPagedResponse<Article>> GetArticlesAsync(int pageSize = 10, int pageNumber = 1);

        Task<ISingleResponse<Article>> GetArticleAsync(Int64 id);

        Task<ISingleResponse<Article>> CreateArticleAsync(Article article);
    }
}
