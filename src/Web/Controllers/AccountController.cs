using System;
using System.Linq;
using System.Globalization;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.Infrastructure.Identity;
using Microsoft.eShopWeb.Web.ViewModels.Manage;
using Microsoft.eShopWeb.Web.ViewModels.Account;
using Microsoft.eShopWeb.Infrastructure.Data;
using Microsoft.Extensions.Configuration;

namespace Microsoft.eShopWeb.Web.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly IAppLogger<ManageController> _logger;
        private readonly UrlEncoder _urlEncoder;
        private readonly CatalogContext _catalogContext;
        
        private readonly IConfiguration _configuration;
  
        public AccountController(
          UserManager<ApplicationUser> userManager,
          SignInManager<ApplicationUser> signInManager,
          IEmailSender emailSender,
          IAppLogger<ManageController> logger,
          UrlEncoder urlEncoder,
          CatalogContext catalogContext,
          IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _logger = logger;
            _urlEncoder = urlEncoder;
            _catalogContext = catalogContext;
            _configuration = configuration;
        }

        [TempData]
        public string StatusMessage { get; set; }
  

        [HttpGet]
        public async Task<IActionResult> LinkLoginCallback()
        {
            _logger.LogInformation("External login inicialized");
            var infoExternelLogin = await _signInManager.GetExternalLoginInfoAsync();
            if (infoExternelLogin == null)
            {
                _logger.LogWarning("External login failed");
                throw new ApplicationException("Unexpected error occurred loading external login.");
            }
            
            string email = infoExternelLogin.Principal.FindFirstValue(ClaimTypes.Email);
            var providerUser = _userManager.Users.Where(x => x.Email == email && x.ProviderName == infoExternelLogin.ProviderDisplayName).ToList().FirstOrDefault();
            
            if(providerUser == null){
                // redirecting to a new user's page
                return LocalRedirect("/Identity/Account/RegisterSocial");
            }
            else {
                var numberDaysActive    = Int32.Parse(_configuration.GetValue<string>("SocialLogin:NumberDaysActive"));
                var registrationDate = providerUser.RegistrationDate;
                var diffDates = DateTime.Now.Subtract(registrationDate);
                if(diffDates.Days > numberDaysActive){
                    // send e-mail for comfirmation
                    providerUser.EmailConfirmed = false;
                    await _userManager.UpdateAsync(providerUser);
                        _logger.LogInformation("User is registered, email confirmation is missing.");

                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(providerUser);
                        var callbackUrl = Url.Page(
                            "/Account/ConfirmEmail",
                            pageHandler: null,
                            values: new { userId = providerUser.Id, code = code },
                            protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(providerUser.Email, "Confirm your email", $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");
                }
            }
            // Sign In
            _logger.LogInformation($"External login accepted for {email} by {infoExternelLogin.ProviderDisplayName}");
            await _signInManager.SignInAsync(providerUser, isPersistent: false);
            return RedirectToAction(nameof(Index));
        }

    }
}