using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Domain.Business.QueryServices;
using Domain.Data.Queries;
using Domain.Data.Queries.ArticleQueries;
using Domain.Entities.Blog;
using EnsureThat;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages
{
    public class SearchModel : PageModel
    {
        private readonly IQueryService<FetchArticlesBySearchQuery, Paged<Article>> search;

        public SearchModel(IQueryService<FetchArticlesBySearchQuery, Paged<Article>> search)
        {
            EnsureArg.IsNotNull(search, nameof(search));

            this.search = search;
        }

        public IActionResult OnGet()
        {
            if (string.IsNullOrEmpty(Text))
            {
                return BadRequest("Search text must not be null or empty");
            }

            var page = search.Execute(new FetchArticlesBySearchQuery
            {
                Paging = new PageInfo
                {
                    PageIndex = CurrentPage - 1,
                    PageSize = 8
                },
                SearchText = Text,
            });

            Articles = page.Items ?? Enumerable.Empty<Article>();

            return Page();
        }

        [BindProperty(SupportsGet = true)]
        [Required(AllowEmptyStrings = false)]
        public string Text { get; set; }

        [BindProperty]
        [Range(0, int.MaxValue, ErrorMessage = "Invalid page number.")]
        public int CurrentPage { get; set; } = 1;
        public bool ShowPrevious => CurrentPage > 1;

        public IEnumerable<Article> Articles { get; set; }
    }
}