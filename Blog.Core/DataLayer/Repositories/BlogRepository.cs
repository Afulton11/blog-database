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

        public Task<Author> GetAuthorAsync(Author entity)
            => DbContext.Set<Author>().FirstOrDefaultAsync(item => item.AuthorID == entity.AuthorID);

        public Task<int> AddAuthorAsync(Author entity)
        {
            Add(entity);

            return CommitChangesAsync();
        }

        public Task<int> UpdateAuthorAsync(Author changes)
        {
            Update(changes);

            return CommitChangesAsync();
        }

        public Task<int> DeleteAuthorAsync(Author entity)
        {
            Remove(entity);

            return CommitChangesAsync();
        }

        public IQueryable<Article> GetArticles()
            => DbContext.Set<Article>();

        public Task<Article> GetArticleAsync(Article entity)
            => DbContext.Set<Article>().FirstOrDefaultAsync(item => item.ArticleID == entity.ArticleID);

        public Task<int> AddArticleAsync(Article entity)
        {
            Add(entity);

            return CommitChangesAsync();
        }

        public Task<int> UpdateArticleAsync(Article changes)
        {
            Update(changes);

            return CommitChangesAsync();
        }

        public Task<int> DeleteArticleAsync(Article entity)
        {
            Remove(entity);

            return CommitChangesAsync();
        }

        public IQueryable<ArticleCategory> GetArticleCategories()
            => DbContext.Set<ArticleCategory>();

        public Task<ArticleCategory> GetArticleCategoryAsync(ArticleCategory entity)
            => DbContext.Set<ArticleCategory>().FirstOrDefaultAsync(item => item.ArticleCategoryID == entity.ArticleCategoryID);

        public Task<int> AddArticleCategoryAsync(ArticleCategory entity)
        {
            Add(entity);

            return CommitChangesAsync();
        }

        public Task<int> UpdateArticleCategoryAsync(ArticleCategory changes)
        {
            Update(changes);

            return CommitChangesAsync();
        }

        public Task<int> DeleteArticleCategoryAsync(ArticleCategory entity)
        {
            Remove(entity);

            return CommitChangesAsync();
        }

        public IQueryable<ContentStatus> GetContentStatuses()
            => DbContext.Set<ContentStatus>();
    }
}
