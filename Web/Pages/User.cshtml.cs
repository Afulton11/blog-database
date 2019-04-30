using System.Collections.Generic;
using System.Linq;
using Domain.Business.QueryServices;
using Domain.Data.Queries;
using Domain.Data.Commands;
using Domain.Data.Queries.AuthorQueries;
using Domain.Data.Queries.PointQueries;
using Domain.Data.Queries.FavoriteQueries;
using Domain.Entities.Blog;
using EnsureThat;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Domain.Business;
using Domain.Entities.View;
using Domain.Data;
using System.Threading.Tasks;

namespace Web.Pages
{
    public class UserModel : PageModel
    {
        private IUserContext UserContext;
        private readonly IQueryService<GetTotalPointsByUserIdQuery, int> GetTotalPoints;
        private readonly IQueryProcessor QueryProcessor;
        private readonly ICommandProcessor CommandProcessor;


        public int? CurrentUserId { get; set; }
        public new User User { get; set; }
        public Author Author { get; set; }
        public IEnumerable<Article> Articles { get; set; }
        public int TotalPoints { get; set; }
        public IEnumerable<PointBreakdown> PointBreakdowns { get; set; }
        public IEnumerable<Article> FavoriteArticles { get; set; }

        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;
        public bool showPrevious => CurrentPage > 1;

        public UserModel
            (
                IUserContext userContext,
                IQueryService<GetTotalPointsByUserIdQuery, int> getTotalPoints,
                IQueryProcessor queryProcessor,
                ICommandProcessor commandProcessor
            )
        {
            EnsureArg.IsNotNull(userContext, nameof(userContext));
            EnsureArg.IsNotNull(getTotalPoints, nameof(getTotalPoints));
            EnsureArg.IsNotNull(queryProcessor, nameof(queryProcessor));
            EnsureArg.IsNotNull(commandProcessor, nameof(commandProcessor));

            UserContext = userContext;
            GetTotalPoints = getTotalPoints;
            QueryProcessor = queryProcessor;
            CommandProcessor = commandProcessor;
        }

        public IActionResult OnGet(int id)
        {
            CurrentUserId = UserContext.CurrentUserId;

            User = SetUser(id);

            if (User != null)
            {
                Author = SetAuthor(User.UserId);

                if (Author != null)
                {
                    Articles = SetArticles(Author.AuthorUserId);
                }

                FavoriteArticles = SetFavoriteArticles(User.UserId);

                TotalPoints = SetTotalPoints(User.UserId);

                PointBreakdowns = SetPointBreakDowns(User.UserId);
            }
            else
            {
                return LocalRedirect("~/");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostDeleteUser()
        {
            var userId = UserContext.CurrentUserId;

            if (ModelState.IsValid)
            {
                await CommandProcessor.Execute(new DeleteUserCommand
                {
                    UserId = (int)userId,
                });
            }

            return Redirect("~/");
        }

        private User SetUser(int id)
        {
            return QueryProcessor.Execute(new FetchUserByIdQuery
            {
                UserId = id
            });
        }

        private Author SetAuthor(int userId)
        {
            return QueryProcessor.Execute(new FetchAuthorByIdQuery
            {
                AuthorId = userId
            });
        }

        private IEnumerable<Article> SetArticles(int authorId)
        {
            return QueryProcessor.Execute(new FetchArticlesByAuthorIdQuery
            {
                AuthorId = authorId,
                Paging = new PageInfo
                {
                    PageIndex = CurrentPage - 1,
                },
            }).Items ?? Enumerable.Empty<Article>();
        }

        private int SetTotalPoints(int userId)
        {
            return GetTotalPoints.Execute(new GetTotalPointsByUserIdQuery
            {
                UserId = userId,
            });
        }

        private IEnumerable<PointBreakdown> SetPointBreakDowns(int userId)
        {
            return QueryProcessor.Execute(new GetPointBreakdownByUserIdQuery
            {
                UserId = userId,
            });
        }

        private IEnumerable<Article> SetFavoriteArticles(int userId)
        {
            return QueryProcessor.Execute(new FetchFavoriteArticlesByUserIdQuery
            {
                UserId = userId,
            });
        }
    }
}
