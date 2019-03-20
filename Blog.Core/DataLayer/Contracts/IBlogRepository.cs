using System.Linq;
using System.Threading.Tasks;
using Blog.Core.EntityLayer.Blog;

namespace Blog.Core.DataLayer.Contracts
{
    public interface IBlogRepository : IRepository
    {
        IQueryable<Author> GetAuthors();

        Task<Author> GetAuthorAsync(Author entity);

        Task<int> AddAuthorAsync(Author entity);

        Task<int> UpdateAuthorAsync(Author changes);

        Task<int> DeleteAuthorAsync(Author entity);

        IQueryable<Article> GetArticles();

        Task<Article> GetArticleAsync(Article entity);

        Task<int> AddArticleAsync(Article entity);

        Task<int> UpdateArticleAsync(Article changes);

        Task<int> DeleteArticleAsync(Article entity);

        IQueryable<ArticleCategory> GetArticleCategories();

        Task<ArticleCategory> GetArticleCategoryAsync(ArticleCategory entity);

        Task<int> AddArticleCategoryAsync(ArticleCategory entity);

        Task<int> UpdateArticleCategoryAsync(ArticleCategory changes);

        Task<int> DeleteArticleCategoryAsync(ArticleCategory entity);

        IQueryable<ContentStatus> GetContentStatuses();
    }
}
