using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.eShopWeb.Infrastructure.Identity;
using Microsoft.eShopWeb.Web.ViewModels.Account;
using Microsoft.Extensions.Logging;

namespace Microsoft.eShopWeb.Web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterSocialModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterSocialModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterSocialModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterSocialModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        public RegisterSocialViewModel RegisterSocialViewModel {get; set; }

        public async Task OnGetAsync()
        {
            var infoExternelLogin = await _signInManager.GetExternalLoginInfoAsync();
            var date = new DateTime();
            DateTime.TryParse(infoExternelLogin.AuthenticationProperties.ExpiresUtc.Value.LocalDateTime.ToString("g", DateTimeFormatInfo.InvariantInfo), out date);

            RegisterSocialViewModel = new RegisterSocialViewModel(){
                Email = infoExternelLogin.Principal.FindFirstValue(ClaimTypes.Email),
                Name = infoExternelLogin.Principal.Identity.Name,
                RegistrationDate = date,
                ProviderName = infoExternelLogin.ProviderDisplayName
            };
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var infoExternelLogin = await _signInManager.GetExternalLoginInfoAsync();
                var date = new DateTime();
                DateTime.TryParse(infoExternelLogin.AuthenticationProperties.ExpiresUtc.Value.LocalDateTime.ToString("g", DateTimeFormatInfo.InvariantInfo), out date);
                var user = new ApplicationUser {
                    UserName         = infoExternelLogin.Principal.FindFirstValue(ClaimTypes.Email),
                    Email            = infoExternelLogin.Principal.FindFirstValue(ClaimTypes.Email),
                    Name             = infoExternelLogin.Principal.Identity.Name,
                    RegistrationDate = date,
                    ProviderName = infoExternelLogin.ProviderDisplayName
                };

                var result = await _userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(user.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    await _signInManager.SignInAsync(user, isPersistent: false);
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
