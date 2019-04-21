using Domain.Business.QueryServices;
using Domain.Data.Queries;
using Domain.Entities.Blog;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Web.Pages
{
    public class ArticleModel : PageModel
    {
        private readonly IQueryService<GetArticleByIdQuery, Article> fetchArticle;

        public ArticleModel(
            IQueryService<GetArticleByIdQuery, Article> fetchArticle)
        {
            this.fetchArticle = fetchArticle;
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

            return Page();
        }

        public Article Article { get; set; }
    }
}