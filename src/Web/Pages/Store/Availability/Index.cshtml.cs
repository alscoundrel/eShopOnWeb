using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.Infrastructure.Identity;
using Microsoft.eShopWeb.Web.Interfaces;
using Microsoft.eShopWeb.Web.Pages.WishList;
using Microsoft.eShopWeb.Web.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.Web.Pages.Store.Availability
{
    public class IndexModel : PageModel
    {

        public IndexModel()
        {
        }

        public StoreItemsAvailabilityViewModel StoreItemsAvailabilityViewModel = new StoreItemsAvailabilityViewModel();

        public void OnGet(int catalogItemId){
            StoreItemsAvailabilityViewModel.CatalogItemId = catalogItemId;
            
        }
    }
}
