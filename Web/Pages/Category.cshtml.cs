using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Domain.Business.QueryServices;
using Domain.Data.Queries;
using Domain.Data.Queries.ArticleCategoryQueries;
using Domain.Data.Queries.ArticleQueries;
using Domain.Entities.Blog;
using EnsureThat;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages
{
    public class CategoryModel : PageModel
    {
        private readonly IQueryService<FetchArticlesByCategoryQuery, Paged<Article>> fetchArticles;
        private readonly IQueryService<FetchArticleCategoryQuery, ArticleCategory> fetchCategory;


        public CategoryModel(
            IQueryService<FetchArticlesByCategoryQuery, Paged<Article>> fetchArticles,
            IQueryService<FetchArticleCategoryQuery, ArticleCategory> fetchCategory)
        {
            EnsureArg.IsNotNull(fetchArticles, nameof(fetchArticles));
            EnsureArg.IsNotNull(fetchCategory, nameof(fetchCategory));

            this.fetchArticles = fetchArticles;
            this.fetchCategory = fetchCategory;
        }

        public IActionResult OnGet()
        {
            var page = fetchArticles.Execute(new FetchArticlesByCategoryQuery
            {
                ArticleCategoryName = Name
            });

            Articles = page.Items ?? Enumerable.Empty<Article>();

            return Page();
        }

        [BindProperty(SupportsGet = true, Name = "name")]
        [Required]
        public string Name { get; set; }

        public IEnumerable<Article> Articles { get; set; }
    }
}