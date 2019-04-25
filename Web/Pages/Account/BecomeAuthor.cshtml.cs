using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Domain.Business;
using Domain.Data;
using Domain.Data.Commands;
using EnsureThat;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Domain.Data.Queries.AuthorQueries;

namespace Web.Pages.Account
{
    public class BecomeAuthorModel : PageModel
    {
        private IUserContext userContext;
        private readonly ICommandProcessor commandProcessor;
        private readonly IAsyncQueryProcessor queryProcessor;

        public BecomeAuthorModel(
            IUserContext userContext,
            ICommandProcessor commandProcessor,
            IAsyncQueryProcessor queryProcessor)
        {
            EnsureArg.IsNotNull(userContext, nameof(userContext));
            EnsureArg.IsNotNull(commandProcessor, nameof(commandProcessor));
            EnsureArg.IsNotNull(queryProcessor, nameof(queryProcessor));

            this.userContext = userContext;
            this.commandProcessor = commandProcessor;
            this.queryProcessor = queryProcessor;
        }

        public async Task OnGetAsync()
        {
            var userId = userContext.CurrentUserId;

            if (userId.HasValue)
            {
                var author = await queryProcessor.ExecuteAsync(new FetchAuthorByIdQuery
                {
                    AuthorId = userId.Value
                });

                AuthorData = new CreateOrUpdateAuthorCommand
                {
                    UserId = userId.Value,
                    FirstName = author.FirstName,
                    LastName = author.LastName,
                    MiddleName = author.MiddleName,
                    BirthDate = author.BirthDate
                };
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var userId = userContext.CurrentUserId;


            if (ModelState.IsValid && userId.HasValue)
            {
                AuthorData.UserId = userId.Value;
                await commandProcessor.Execute(AuthorData);

                return LocalRedirect("~/");
            }
            else
            {
                ModelState.AddModelError("", "Invalid Author Data");
            }

            return Page();
        }

        [BindProperty]
        [Required]
        public CreateOrUpdateAuthorCommand AuthorData { get; set; }


    }
}