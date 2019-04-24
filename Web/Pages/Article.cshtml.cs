using Domain.Business;
using Domain.Data;
using Domain.Data.Commands.Comments;
using Domain.Data.Queries;
using Domain.Data.Queries.ArticleCategoryQueries;
using Domain.Data.Queries.CommentQueries;
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
    public class ArticleModel : PageModel
    {
        private readonly IUserContext userContext;
        private readonly IAsyncQueryProcessor queryProcessor;
        private readonly ICommandProcessor commandProcessor;

        public ArticleModel(
            IUserContext userContext,
            IAsyncQueryProcessor queryProcessor,
            ICommandProcessor commandProcessor)
        {
            EnsureArg.IsNotNull(userContext, nameof(userContext));
            EnsureArg.IsNotNull(queryProcessor, nameof(queryProcessor));
            EnsureArg.IsNotNull(commandProcessor, nameof(commandProcessor));

            this.userContext = userContext;
            this.queryProcessor = queryProcessor;
            this.commandProcessor = commandProcessor;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (userContext.CurrentUserId.HasValue)
            {
                AddCommentModel = new CreateOrUpdateCommentCommand
                {
                    UserId = userContext.CurrentUserId.Value,
                    ArticleId = id
                };
            }

            Article = await queryProcessor.ExecuteAsync(new GetArticleByIdQuery
                {
                    ArticleID = id
                });

            if (Article == null)
            {
                return RedirectToPage("/Index");
            }

            Category = await queryProcessor.ExecuteAsync(new FetchArticleCategoryQuery
            {
                ArticleCategoryId = Article.CategoryId
            });

            var commentPage = await queryProcessor.ExecuteAsync(new FetchArticleCommentsQuery
                {
                    ArticleId = Article.ArticleId,
                    Paging = new PageInfo
                    {
                        PageIndex = CurrentPage - 1,
                        PageSize = 5,
                    }
                });

            Comments = commentPage.Items ?? Enumerable.Empty<ArticleComment>();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var redirectUrl = "~/";

            if (AddCommentModel?.ArticleId != null)
            {
                redirectUrl = $"~/Article/{AddCommentModel.ArticleId}";
            }
            
            if (ModelState.IsValid)
            {
                await commandProcessor.Execute(AddCommentModel);
            }


            return LocalRedirect(redirectUrl);
        }

        [BindProperty]
        public CreateOrUpdateCommentCommand AddCommentModel { get; set; }

        public Article Article { get; set; }

        public ArticleCategory Category { get; set; }

        public IEnumerable<ArticleComment> Comments { get; set; }

        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;

        public bool CanShowPreviousComments => CurrentPage > 1;
        public bool CanShowMoreComments => true;

        public IEnumerable<IList<ArticleComment>> GetCommentThreads()
        {
            var thread = new List<ArticleComment>();

            foreach (var comment in Comments)
            {
                if (comment.ParentCommentId == null && thread.Count > 0)
                {
                    yield return thread;
                    thread = new List<ArticleComment>();
                }

                thread.Add(comment);
            }

            if (thread.Count > 0)
            {
                yield return thread;
            }
        }
    }
}