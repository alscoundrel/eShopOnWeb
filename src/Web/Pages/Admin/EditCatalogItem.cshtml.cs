using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.eShopWeb.ApplicationCore.Constants;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.Web.Interfaces;
using Microsoft.eShopWeb.Web.Services;
using Microsoft.eShopWeb.Web.ViewModels;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.Web.Pages.Admin
{
    [Authorize(Roles = AuthorizationConstants.Roles.ADMINISTRATORS)]
    public class EditCatalogItemModel : PageModel
    {
        private readonly ICatalogItemViewModelService _catalogItemViewModelService;
        private readonly CatalogNotifications _catalogNotifications;

        public EditCatalogItemModel(ICatalogItemViewModelService catalogItemViewModelService, CatalogNotifications catalogNotifications)
        {
            _catalogItemViewModelService = catalogItemViewModelService;
            _catalogNotifications = catalogNotifications;
        }

        [BindProperty]
        public CatalogItemViewModel CatalogModel { get; set; } = new CatalogItemViewModel();

        public void OnGet(CatalogItemViewModel catalogModel)
        {
            CatalogModel = catalogModel;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                await _catalogItemViewModelService.UpdateCatalogItem(CatalogModel);
                await _catalogNotifications.CatalogItemsNotifyAsync(CatalogModel.Id, CatalogModel.Price);
            }

            return RedirectToPage("/Admin/Index");
        }
    }
}
