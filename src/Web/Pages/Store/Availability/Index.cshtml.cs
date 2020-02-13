using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.Infrastructure.Identity;
using Microsoft.eShopWeb.Web.Interfaces;
using Microsoft.eShopWeb.Web.Pages.WishList;
using Microsoft.eShopWeb.Web.Services;
using Microsoft.eShopWeb.Web.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.Web.Pages.Store.Availability
{
    public class IndexModel : PageModel
    {
        private readonly IStoreViewModelService _storeViewModelService;

        public IndexModel(IStoreViewModelService storeViewModelService)
        {
            _storeViewModelService = storeViewModelService;
        }

        public StoreItemsAvailabilityViewModel StoreItemsAvailabilityViewModel = new StoreItemsAvailabilityViewModel();

        public async Task OnGetAsync(int catalogItemId){
            StoreItemsAvailabilityViewModel = await _storeViewModelService.GetItemAvailabilityByStore(catalogItemId);
        }
    }
}
