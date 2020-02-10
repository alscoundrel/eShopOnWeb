using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.Infrastructure.Identity;
using Microsoft.eShopWeb.Web.Interfaces;
using Microsoft.eShopWeb.Web.ViewModels;
using System;
using System.Linq;
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

        public async Task OnGet()
        {
            WishListModel = await _wishListViewModelService.GetOrCreateWishListForUser(User.Identity.Name);
        }

        /// <summary>
        /// Insert CatalogItem to WishList
        /// Get or creat wish list for user
        /// </summary>
        /// <param name="productDetails"></param>
        /// <returns></returns>
        public async Task<IActionResult> OnPost(CatalogItemViewModel productDetails)
        {
            if (productDetails?.Id == null)
            {
                return RedirectToPage("/Index");
            }
            if (_signInManager.IsSignedIn(HttpContext.User)){
                WishListModel = await _wishListViewModelService.GetOrCreateWishListForUser(User.Identity.Name);
                await _wishListService.AddItemToWishList(WishListModel.Id, productDetails.Id, productDetails.Price, productDetails.PriceSymbol);
                WishListModel = await _wishListViewModelService.GetOrCreateWishListForUser(User.Identity.Name);
                return RedirectToPage();
            }
            else{
                throw new Exception("You don't have valid authentication!");
            }
        }

        /// <summary>
        /// Update WishList
        /// </summary>
        /// <param name="catalogItemId"></param>
        /// <param name="notifyCasePriceChanges"></param>
        /// <returns></returns>
        public async Task<IActionResult> OnPostUpdate(int catalogItemId, bool notifyCasePriceChanges){
            WishListModel = await _wishListViewModelService.GetOrCreateWishListForUser(User.Identity.Name);

            var WishItem = WishListModel.Items.Where(x => x.CatalogItemId ==catalogItemId).FirstOrDefault();
            if(WishItem !=null && WishItem.NotifyCasePriceChanges!=notifyCasePriceChanges){
                await _wishListService.SetNotifies(WishListModel.Id, catalogItemId, notifyCasePriceChanges);
                WishListModel = await _wishListViewModelService.GetOrCreateWishListForUser(User.Identity.Name);
            }
            return RedirectToPage();
        }

        /// <summary>
        /// Update wishList name
        /// </summary>
        /// <param name="wishName"></param>
        /// <returns></returns>
        public async Task<IActionResult> OnPostUpdateName(string wishName){
            WishListModel = await _wishListViewModelService.GetOrCreateWishListForUser(User.Identity.Name);

            var cleanWishName = string.IsNullOrEmpty(wishName)?"":wishName.Trim();
            if(WishListModel.WishName != cleanWishName){
                await _wishListService.SetWishName(WishListModel.Id, cleanWishName);
                WishListModel = await _wishListViewModelService.GetOrCreateWishListForUser(User.Identity.Name);
            }
            return RedirectToPage();
        }

        /// <summary>
        /// Delete wishList
        /// </summary>
        /// <param name="checkDelete">authorizes to eliminate</param>
        /// <returns></returns>
        public async Task<IActionResult> OnPostDelete(string checkDelete){
            WishListModel = await _wishListViewModelService.GetOrCreateWishListForUser(User.Identity.Name);
            if(checkDelete == "on"){
                await _wishListService.DeleteWishListAsync(WishListModel.Id);
                WishListModel = await _wishListViewModelService.GetOrCreateWishListForUser(User.Identity.Name);
            }
            return RedirectToPage();
        }
    }
}
