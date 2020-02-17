using System;
using Microsoft.AspNetCore.Identity;

namespace Microsoft.eShopWeb.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string ProviderName { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}
