using System.Threading.Tasks;
using Domain.Entities.Blog;
using EnsureThat;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages
{
    public class LogoutModel : PageModel
    {
        private SignInManager<User> signInManager;

        public LogoutModel(
            SignInManager<User> signInManager)
        {
            EnsureArg.IsNotNull(signInManager, nameof(signInManager));

            this.signInManager = signInManager;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            await signInManager.SignOutAsync();
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }

            return Page();
        }
    }
}