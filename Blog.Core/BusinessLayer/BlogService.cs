using System;
using System.Linq;
using System.Threading.Tasks;
using Blog.Core.BusinessLayer.Contracts;
using Blog.Core.BusinessLayer.Responses;
using Blog.Core.DataLayer;
using Blog.Core.EntityLayer.Blog;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Blog.Core.BusinessLayer
{
    public sealed class BlogService : Service, IBlogService
    {
        public BlogService(ILogger logger, IUserInfo userInfo, BlogDbContext dbcontext)
            : base(logger, userInfo, dbcontext)
        {
        }

        public async Task<IPagedResponse<Author>> GetAuthorsAsync(int pageSize = 10, int pageNumber = 1)
        {
            Logger?.LogDebug($"{nameof(GetAuthorsAsync)} has been invoked");

            var response = new PagedResponse<Author>();

            try
            {
                var query = BlogRepository.GetAuthors();

                // set paging information
                response.PageSize = pageSize;
                response.PageNumber = pageNumber;
                response.ItemsCount = await query.CountAsync();

                // Retrieve items, set model for response.
                response.Model = await query
                    .Paging(pageSize, pageNumber)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                response.SetError(ex, Logger);
            }

            return response;
        }

        public Task<ISingleResponse<Author>> GetAuthorAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<IPagedResponse<Article>> GetArticlesAsync(int pageSize = 10, int pageNumber = 1)
        {
            throw new NotImplementedException();
        }

        public Task<ISingleResponse<Article>> GetArticleAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<ISingleResponse<Article>> CreateArticleAsync(Article article)
        {
            throw new NotImplementedException();
        }
    }
}
