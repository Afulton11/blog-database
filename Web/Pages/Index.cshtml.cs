using Domain.Business.QueryServices;
using Domain.Data.Queries;
using Domain.Data.Queries.ArticleQueries;
using Domain.Entities.Blog;
using EnsureThat;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

namespace Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IQueryService<FetchRecentArticlesQuery, Paged<Article>> fetchRecentArticles;

        public IndexModel(
            IQueryService<FetchRecentArticlesQuery, Paged<Article>> fetchRecentArticles)
        {
            EnsureArg.IsNotNull(fetchRecentArticles, nameof(fetchRecentArticles));
            this.fetchRecentArticles = fetchRecentArticles;
        }

        public void OnGet()
        {
            var page = fetchRecentArticles.Execute(
                new FetchRecentArticlesQuery
                {
                    Paging = new PageInfo
                    {
                        PageIndex = CurrentPage - 1,
                        PageSize = 8
                    }
                });

            Articles = page.Items ?? Enumerable.Empty<Article>();
        }

        public IEnumerable<Article> Articles { get; private set; }

        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;
        public bool ShowPrevious => CurrentPage > 1;
            
    }
}
