using Domain.Business;
using Domain.Business.QueryServices;
using Domain.Data.Queries;
using Domain.Data.Queries.ArticleQueries;
using Domain.Entities.Blog;
using Domain.Entities.View;
using EnsureThat;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IAsyncQueryProcessor queryProcessor;

        public IndexModel(
            IAsyncQueryProcessor queryProcessor)
        {
            EnsureArg.IsNotNull(queryProcessor, nameof(queryProcessor));
            this.queryProcessor = queryProcessor;
        }

        public async Task OnGetAsync()
        {
            var page = await queryProcessor.ExecuteAsync(
                new FetchRecentArticlesPreviewQuery
                {
                    Paging = new PageInfo
                    {
                        PageIndex = CurrentPage - 1,
                        PageSize = 8
                    }
                });

            Articles = page.Items ?? Enumerable.Empty<PreviewArticle>();
        }

        public IEnumerable<PreviewArticle> Articles { get; private set; }

        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;
        public bool ShowPrevious => CurrentPage > 1;
    }
}
