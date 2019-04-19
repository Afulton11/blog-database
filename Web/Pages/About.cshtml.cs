using Domain.Business.QueryServices;
using Domain.Data.Queries;
using Domain.Entities.Blog;
using EnsureThat;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Web.Pages
{
    public class AboutModel : PageModel
    {
        public string Message { get; set; }

        public Article Article { get; set; }

        private readonly IQueryService<GetArticleByIdQuery, Article> getArticle;

        private ILogger Logger { get; }

        public AboutModel(
            ILogger<AboutModel> logger,
            IQueryService<GetArticleByIdQuery, Article> getArticle)
        {
            EnsureArg.IsNotNull(logger, nameof(logger));
            EnsureArg.IsNotNull(getArticle, nameof(getArticle));

            this.getArticle = getArticle;

            Logger = logger;
        }

        public void OnGet()
        {
            Message = "Your application description page.";
            Article = getArticle.Execute(new GetArticleByIdQuery
            {
                ArticleID = 1
            });
        }
    }
}
