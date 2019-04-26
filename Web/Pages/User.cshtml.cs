using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Domain.Business.QueryServices;
using Domain.Data.Queries;
using Domain.Data.Queries.UserQueries;
using Domain.Data.Queries.AuthorQueries;
using Domain.Data.Queries.ArticleQueries;
using Domain.Data.Queries.PointQueries;
using Domain.Entities.Blog;
using Domain.Entities.View;
using EnsureThat;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages
{
    public class UserModel : PageModel
    {
        private readonly IQueryService<FetchUserByIdQuery, User> FetchUser;
        private readonly IQueryService<FetchAuthorByIdQuery, Author> FetchAuthor;
        private readonly IQueryService<FetchArticlesByAuthorIdQuery, Paged<Article>> FetchArticles;
        private readonly IQueryService<GetTotalPointsByUserIdQuery, int> GetTotalPoints;
        private readonly IQueryService<GetPointBreakdownByUserIdQuery, IEnumerable<PointBreakdown>> GetPointBreakdown;

        public new User User { get; set; }
        public Author Author { get; set; }
        public IEnumerable<Article> Articles { get; set; }
        public int TotalPoints { get; set; }
        public IEnumerable<PointBreakdown> PointBreakdowns { get; set; }

        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;
        public bool showPrevious => CurrentPage > 1;

        public UserModel
            (
                IQueryService<FetchUserByIdQuery, User> fetchUser,
                IQueryService<FetchAuthorByIdQuery, Author> fetchAuthor,
                IQueryService<FetchArticlesByAuthorIdQuery, Paged<Article>> fetchArticles,
                IQueryService<GetTotalPointsByUserIdQuery, int> getTotalPoints,
                IQueryService<GetPointBreakdownByUserIdQuery, IEnumerable<PointBreakdown>> getPointBreakdown
            )
        {
            EnsureArg.IsNotNull(fetchUser, nameof(fetchUser));
            EnsureArg.IsNotNull(fetchAuthor, nameof(fetchAuthor));
            EnsureArg.IsNotNull(fetchArticles, nameof(fetchArticles));
            EnsureArg.IsNotNull(getTotalPoints, nameof(getTotalPoints));
            EnsureArg.IsNotNull(getPointBreakdown, nameof(getPointBreakdown));

            FetchUser = fetchUser;
            FetchAuthor = fetchAuthor;
            FetchArticles = fetchArticles;
            GetTotalPoints = getTotalPoints;
            GetPointBreakdown = getPointBreakdown;
        }

        public void OnGet(int id)
        {
            User = SetUser(id);

            if (User != null)
            {
                Author = SetAuthor(User.UserId);

                if (Author != null)
                {
                    Articles = SetArticles(Author.AuthorUserId);
                }

                TotalPoints = SetTotalPoints(User.UserId);

                PointBreakdowns = SetPointBreakDowns(User.UserId);
            }
        }

        private User SetUser(int id)
        {
            return FetchUser.Execute(new FetchUserByIdQuery
            {
                UserId = id
            });
        }

        private Author SetAuthor(int userId)
        {
            return FetchAuthor.Execute(new FetchAuthorByIdQuery
            {
                AuthorId = userId
            });
        }

        private IEnumerable<Article> SetArticles(int authorId)
        {
            return FetchArticles.Execute(new FetchArticlesByAuthorIdQuery
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
            return GetPointBreakdown.Execute(new GetPointBreakdownByUserIdQuery
            {
                UserId = userId,
            });
        }
    }
}
