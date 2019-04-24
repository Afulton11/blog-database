using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Domain.Business.QueryServices;
using Domain.Data.Queries;
using Domain.Data.Queries.UserQueries;
using Domain.Entities.Blog;
using EnsureThat;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages
{
    public class UserModel : PageModel
    {
        private readonly IQueryService<FetchUserByIdQuery, User> FetchUser;
        public User User { get; set; }

        public UserModel(IQueryService<FetchUserByIdQuery, User> fetchUser)
        {
            EnsureArg.IsNotNull(fetchUser, nameof(fetchUser));

            FetchUser = fetchUser;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            User = await Task.Factory.StartNew(() =>
            {
                return FetchUser.Execute(
                    new FetchUserByIdQuery
                    {
                        UserId = id
                    });
            });

            if (User == null)
            {
                return RedirectToPage("/Index");
            }

            return Page();
        }
    }
}
