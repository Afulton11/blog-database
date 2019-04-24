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
using Domain.Entities.Blog;
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

        public new User User { get; set; }
        public Author Author { get; set; }
        public IEnumerable<Article> Articles { get; set; }

        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;
        public bool showPrevious => CurrentPage > 1;

        public UserModel
            (
                IQueryService<FetchUserByIdQuery, User> fetchUser,
                IQueryService<FetchAuthorByIdQuery, Author> fetchAuthor,
                IQueryService<FetchArticlesByAuthorIdQuery, Paged<Article>> fetchArticles
            )
        {
            EnsureArg.IsNotNull(fetchUser, nameof(fetchUser));
            EnsureArg.IsNotNull(fetchAuthor, nameof(fetchAuthor));
            EnsureArg.IsNotNull(fetchArticles, nameof(fetchArticles));

            FetchUser = fetchUser;
            FetchAuthor = fetchAuthor;
            FetchArticles = fetchArticles;
        }

        public void OnGet(int id)
        {
            User = FetchUser.Execute(new FetchUserByIdQuery
            {
                UserId = id
            });

            if (User != null)
            {
                Author = FetchAuthor.Execute(new FetchAuthorByIdQuery
                {
                    AuthorId = User.UserId
                });

                if (Author != null)
                {
                    Articles = FetchArticles.Execute(new FetchArticlesByAuthorIdQuery
                    {
                        AuthorId = Author.AuthorUserId,
                        Paging = new PageInfo
                        {
                            PageIndex = CurrentPage - 1,
                        },
                    }).Items ?? Enumerable.Empty<Article>();
                }
            }
        }
    }
}
