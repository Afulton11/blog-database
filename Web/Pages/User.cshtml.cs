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
using EnsureThat;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Domain.Business;

namespace Web.Pages
{
    public class UserModel : PageModel
    {
        private readonly IQueryService<GetTotalPointsByUserIdQuery, int> GetTotalPoints;
        private readonly IQueryProcessor QueryProcessor;

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
                IQueryService<GetTotalPointsByUserIdQuery, int> getTotalPoints,
                IQueryProcessor queryProcessor
            )
        {
            EnsureArg.IsNotNull(getTotalPoints, nameof(getTotalPoints));
            EnsureArg.IsNotNull(queryProcessor, nameof(queryProcessor));

            GetTotalPoints = getTotalPoints;
            QueryProcessor = queryProcessor;
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
    }
}
