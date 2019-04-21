using Domain.Business.QueryServices;
using Domain.Data.Queries;
using Domain.Data.Queries.ArticleCategoryQueries;
using Domain.Entities.Blog;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Web.Pages
{
    public class ArticleModel : PageModel
    {
        private readonly IQueryService<GetArticleByIdQuery, Article> fetchArticle;
        private readonly IQueryService<FetchArticleCategoryQuery, ArticleCategory> fetchCategory;

        public ArticleModel(
            IQueryService<GetArticleByIdQuery, Article> fetchArticle,
            IQueryService<FetchArticleCategoryQuery, ArticleCategory> fetchCategory)
        {
            this.fetchArticle = fetchArticle;
            this.fetchCategory = fetchCategory;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Article = await Task.Factory.StartNew(() =>
            {
                return fetchArticle.Execute(
                    new GetArticleByIdQuery
                    {
                        ArticleID = id
                    });
            });

            if (Article == null)
            {
                return RedirectToPage("/Index");
            }

            Category = fetchCategory.Execute(new FetchArticleCategoryQuery
            {
                ArticleCategoryId = Article.CategoryId
            });

            return Page();
        }

        public Article Article { get; set; }

        public ArticleCategory Category { get; set; }
            

    }
}