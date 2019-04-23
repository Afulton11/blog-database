using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Domain.Business;
using Domain.Data;
using Domain.Data.Commands.Articles;
using Domain.Data.Queries;
using Domain.Data.Queries.ArticleCategoryQueries;
using Domain.Entities.Blog;
using EnsureThat;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Pages.Edit
{
    public class ArticleModel : PageModel
    {
        private IUserContext userContext;
        private readonly ICommandProcessor commandProcessor;
        private readonly IQueryProcessor queryProcessor;

        public ArticleModel(
            IUserContext userContext,
            ICommandProcessor commandProcessor,
            IQueryProcessor queryProcessor)
        {
            EnsureArg.IsNotNull(userContext, nameof(userContext));
            EnsureArg.IsNotNull(commandProcessor, nameof(commandProcessor));
            EnsureArg.IsNotNull(queryProcessor, nameof(queryProcessor));

            this.userContext = userContext;
            this.commandProcessor = commandProcessor;
            this.queryProcessor = queryProcessor;
        }

        public void OnGet(int? id)
        {

            if (id.HasValue)
            {
                ArticleId = id;

                var currentArticle = queryProcessor.Execute(new GetArticleByIdQuery
                {
                    ArticleID = ArticleId.Value
                });

                ArticleData = new CreateArticleData
                {
                    Title = currentArticle.Title,
                    ContentStatus = currentArticle.ContentStatus?.Value,
                    Description = currentArticle.Description,
                    Body = currentArticle.Body,
                    ArticleCategory = queryProcessor.Execute(new FetchArticleCategoryQuery
                    {
                        ArticleCategoryId = currentArticle.CategoryId
                    })?.Name,
                };
            }

            ArticleCategories = queryProcessor.Execute(new FetchArticleCategoriesQuery());
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var userId = userContext.CurrentUserId;

            if (ModelState.IsValid)
            {
                var article = new Article
                {
                    AuthorId = userId ?? -1,
                    Title = ArticleData.Title,
                    Description = ArticleData.Description,
                    Body = ArticleData.Body,
                    ContentStatus = (ContentStatus)ArticleData.ContentStatus,
                    CategoryId = int.Parse(ArticleData.ArticleCategory),
                };

                if (ArticleId != null)
                {
                    article.ArticleId = ArticleId.Value;
                }

                await commandProcessor.Execute(new CreateOrUpdateArticleCommand
                {
                    Article = article
                });

                if (ArticleId != null)
                {
                    return LocalRedirect($"~/Article/{ArticleId}");
                }
                else
                {
                    return LocalRedirect("~/");
                }
            }
            else
            {
                ModelState.AddModelError("", "Invalid Article");
            }

            return Page();
        }

        [BindProperty]
        [Required]
        public CreateArticleData ArticleData { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? ArticleId { get; set; }

        public IEnumerable<ArticleCategory> ArticleCategories { get; set; }

        public IEnumerable<SelectListItem> GetCategorySelectList()
        {
            foreach (var category in ArticleCategories)
            {
                yield return new SelectListItem
                {
                    Value = category.ArticleCategoryID.ToString(),
                    Text = category.Name
                };
            }
        }

        public IEnumerable<SelectListItem> GetContentStatusSelectList()
        {
            foreach (var status in ContentStatus.AllStatuses)
            {
                yield return new SelectListItem
                {
                    Value = status.Value,
                    Text = status.Value
                };
            }
        }

    }

    public class CreateArticleData
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string ArticleCategory { get; set; } = "History";
        [Required]
        public string ContentStatus { get; set; } = Domain.Entities.Blog.ContentStatus.Draft.Value;
        [Required]
        public string Description { get; set; }
        [Required]
        public string Body { get; set; }
    }
}