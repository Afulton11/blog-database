﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Domain.Data.Commands;
using Domain.Entities.Blog;
using EnsureThat;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages
{
    public class RegisterModel : PageModel
    {
        private UserManager<User> userManager;
        private SignInManager<User> signInManager;
        private IEmailSender emailSender;

        public RegisterModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IEmailSender emailSender)
        {
            EnsureArg.IsNotNull(userManager, nameof(userManager));
            EnsureArg.IsNotNull(signInManager, nameof(signInManager));
            EnsureArg.IsNotNull(emailSender, nameof(emailSender));

            this.userManager = userManager;
            this.signInManager = signInManager;
            this.emailSender = emailSender;
        }
        
        [BindProperty]
        public CreateUserCommand RegisterData { get; set; }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    Username = RegisterData.Username,
                    Email = RegisterData.Email,
                };

                var result = await userManager.CreateAsync(user, RegisterData.Password);

                if (result.Succeeded)
                {
                    var code = await userManager.GenerateEmailConfirmationTokenAsync(user);

                    var callbackUrl = Url.Page(
                        "/Index",
                        pageHandler: null,
                        values: new { username = user.Username, code },
                        protocol: Request.Scheme);

                    await emailSender.SendEmailAsync(RegisterData.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    await signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
