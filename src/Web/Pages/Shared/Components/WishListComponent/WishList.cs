using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.eShopWeb.Infrastructure.Identity;
using Microsoft.eShopWeb.Web.Interfaces;
using Microsoft.eShopWeb.Web.Pages.WishList;
using Microsoft.eShopWeb.Web.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.Web.Pages.Shared.Components.WishListComponent
{
    public class WishList : ViewComponent
    {
        private readonly IWishListViewModelService _wishListService;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public WishList(IWishListViewModelService wishListService,
                        SignInManager<ApplicationUser> signInManager)
        {
            _wishListService = wishListService;
            _signInManager = signInManager;
        }

        public async Task<IViewComponentResult> InvokeAsync(string userName)
        {
            var vm = new WishListComponentViewModel();
            vm.ItemsCount = (await GetWishListViewModelAsync()).Items.Sum(i => i.Quantity);
            return View(vm);
        }

        private async Task<WishListViewModel> GetWishListViewModelAsync()
        {
            if (_signInManager.IsSignedIn(HttpContext.User))
            {
                return await _wishListService.GetOrCreateWishListForUser(User.Identity.Name);
            }
            throw new Exception("You don't have valid authentication!");
        }
    }
}
