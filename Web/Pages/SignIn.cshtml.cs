using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities.Blog;
using EnsureThat;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages
{
    public class SignInModel : PageModel
    {
        private readonly SignInManager<User> signInManager;

        public SignInModel(SignInManager<User> signInManager)
        {
            EnsureArg.IsNotNull(signInManager, nameof(signInManager));

            this.signInManager = signInManager;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(
                    LoginData.Username,
                    LoginData.Password,
                    LoginData.RememberMe,
                    lockoutOnFailure: true);

                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return LocalRedirect(returnUrl);
                    }

                    return LocalRedirect("~/");
                }
                else if (result.IsLockedOut)
                {
                    // TODO: redirect to lockout page.
                    ModelState.AddModelError("", "Invalid login. You are locked out.");
                }
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt");
            return Page();
        }

        [BindProperty]
        public LoginData LoginData { get; set; }
    }

    public class LoginData
    {
        [Required]
        public string Username { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}
