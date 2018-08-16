using System.Linq;
using System.Threading.Tasks;
using Blog.Core.DataLayer.Contracts;
using Blog.Core.EntityLayer.Blog;
using Microsoft.EntityFrameworkCore;

namespace Blog.Core.DataLayer.Repositories
{
    public sealed class BlogRepository : Repository, IBlogRepository
    {
        public BlogRepository(IUserInfo userInfo, DbContext dbContext)
            : base(userInfo, dbContext)
        {
        }

        public IQueryable<Author> GetAuthors()
            => DbContext.Set<Author>();

        public async Task<Author> GetAuthorAsync(Author entity)
            => await DbContext.Set<Author>().FirstOrDefaultAsync(item => item.AuthorID == entity.AuthorID);

        public async Task<int> AddAuthorAsync(Author entity)
        {
            Add(entity);

            return await CommitChangesAsync();
        }

        public async Task<int> UpdateAuthorAsync(Author changes)
        {
            Update(changes);

            return await CommitChangesAsync();
        }

        public async Task<int> DeleteAuthorAsync(Author entity)
        {
            Remove(entity);

            return await CommitChangesAsync();
        }

        public IQueryable<Article> GetArticles()
            => DbContext.Set<Article>();

        public async Task<Article> GetArticleAsync(Article entity)
            => await DbContext.Set<Article>().FirstOrDefaultAsync(item => item.ArticleID == entity.ArticleID);

        public async Task<int> AddArticleAsync(Article entity)
        {
            Add(entity);

            return await CommitChangesAsync();
        }

        public async Task<int> UpdateArticleAsync(Article changes)
        {
            Update(changes);

            return await CommitChangesAsync();
        }

        public async Task<int> DeleteArticleAsync(Article entity)
        {
            Remove(entity);

            return await CommitChangesAsync();
        }

        public IQueryable<ArticleCategory> GetArticleCategories()
            => DbContext.Set<ArticleCategory>();

        public async Task<ArticleCategory> GetArticleCategoryAsync(ArticleCategory entity)
            => await DbContext.Set<ArticleCategory>().FirstOrDefaultAsync(item => item.ArticleCategoryID == entity.ArticleCategoryID);

        public async Task<int> AddArticleCategoryAsync(ArticleCategory entity)
        {
            Add(entity);

            return await CommitChangesAsync();
        }

        public async Task<int> UpdateArticleCategoryAsync(ArticleCategory changes)
        {
            Update(changes);

            return await CommitChangesAsync();
        }

        public async Task<int> DeleteArticleCategoryAsync(ArticleCategory entity)
        {
            Remove(entity);

            return await CommitChangesAsync();
        }

        public IQueryable<ContentStatus> GetContentStatuses()
            => DbContext.Set<ContentStatus>();
    }
}
