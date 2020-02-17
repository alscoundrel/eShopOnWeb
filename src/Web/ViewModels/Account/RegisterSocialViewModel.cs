using System.ComponentModel.DataAnnotations;

namespace Microsoft.eShopWeb.Web.ViewModels.Account
{
    public class RegisterSocialViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Date")]
        public System.DateTime RegistrationDate { get; set; }

        [Required]
        [Display(Name = "Provider Name")]
        public string ProviderName { get; set; }

    }
}
