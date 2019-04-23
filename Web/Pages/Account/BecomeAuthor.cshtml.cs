using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Domain.Business;
using Domain.Data;
using Domain.Data.Commands;
using EnsureThat;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Account
{
    public class BecomeAuthorModel : PageModel
    {
        private IUserContext userContext;
        private readonly ICommandProcessor commandProcessor;
        private readonly IQueryProcessor queryProcessor;

        public BecomeAuthorModel(
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

        public void OnGet()
        {
            var userId = userContext.CurrentUserId;

            AuthorData = new CreateOrUpdateAuthorCommand
            {
                UserId = userId ?? -1,
            };
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var userId = userContext.CurrentUserId;


            if (ModelState.IsValid)
            {
                AuthorData.UserId = userId ?? -1;
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