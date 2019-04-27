using Domain.Business;
using Domain.Data;
using Domain.Data.Commands.Comments;
using Domain.Data.Commands.Favorite;
using Domain.Data.Queries;
using Domain.Data.Queries.CommentQueries;
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

        public async Task<IActionResult> OnGetAsync(int id, int? parentCommentId)
        {
            Article = await queryProcessor.ExecuteAsync(new FetchArticlePageByIdQuery
                {
                    ArticleId = id,
                    UserId = userContext.CurrentUserId
                });

            if (Article == null)
            {
                return RedirectToPage("/Index");
            }

            if (userContext.CurrentUserId.HasValue)
            {
                AddCommentModel = new CreateOrUpdateCommentCommand
                {
                    UserId = userContext.CurrentUserId.Value,
                    ArticleId = id,
                    ParentCommentId = parentCommentId
                };
            }

            var commentPage = await queryProcessor.ExecuteAsync(new FetchArticleCommentsQuery
                {
                    ArticleId = Article.ArticleId,
                    Paging = new PageInfo
                    {
                        PageIndex = 0,
                        PageSize = PageSize * CurrentPage,
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

        public async Task<IActionResult> OnPostAddUserFavoriteAsync(CreateFavoriteCommand createFavorite)
        {
            var redirectUrl = "~/";
            if (createFavorite != null)
            {
                redirectUrl = $"~/Article/{createFavorite.ArticleId}";
                await commandProcessor.Execute(createFavorite);
            }

            return LocalRedirect(redirectUrl);
        }

        public async Task<IActionResult> OnPostDeleteUserFavoriteAsync(DeleteFavoriteCommand deleteFavorite)
        {
            var redirectUrl = "~/";
            if (deleteFavorite != null)
            {
                redirectUrl = $"~/Article/{deleteFavorite.ArticleId}";
                await commandProcessor.Execute(deleteFavorite);
            }

            return LocalRedirect(redirectUrl);
        }

        [BindProperty]
        public CreateOrUpdateCommentCommand AddCommentModel { get; set; }

        public ArticleViewModel Article { get; set; }

        public IEnumerable<ArticleComment> Comments { get; set; }

        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;

        public int PageSize { get; set; } = 5;

        public bool CanShowPreviousComments => CurrentPage > 1;
        public bool CanShowMoreComments => CurrentPage < (float)(Article.CommentCount / PageSize);
    }
}