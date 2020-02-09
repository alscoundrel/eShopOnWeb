using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.Infrastructure.Identity;
using Microsoft.eShopWeb.Web.Interfaces;
using Microsoft.eShopWeb.Web.ViewModels;
using System;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.Web.Pages.WishList
{
    public class IndexModel : PageModel
    {
        private readonly IWishListService _wishListService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IWishListViewModelService _wishListViewModelService;

        public IndexModel(IWishListService wishListService,
            IWishListViewModelService wishListViewModelService,
            SignInManager<ApplicationUser> signInManager)
        {
            _wishListService = wishListService;
            _signInManager = signInManager;
            _wishListViewModelService = wishListViewModelService;
        }

        public WishListViewModel WishListModel { get; set; } = new WishListViewModel();

        public async Task<IActionResult> OnPost(CatalogItemViewModel productDetails)
        {
            if (productDetails?.Id == null)
            {
                return RedirectToPage("/Index");
            }
            if (_signInManager.IsSignedIn(HttpContext.User)){
                WishListModel = await _wishListViewModelService.GetOrCreateWishListForUser(User.Identity.Name);
                await _wishListService.AddItemToWishList(WishListModel.Id, productDetails.Id, productDetails.Price);
                WishListModel = await _wishListViewModelService.GetOrCreateWishListForUser(User.Identity.Name);
                return RedirectToPage();
            }
            else{
                throw new Exception("You don't have valid authentication!");
            }
        }
    }
}
