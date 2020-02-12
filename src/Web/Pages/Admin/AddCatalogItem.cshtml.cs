using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.eShopWeb.ApplicationCore.Constants;
using Microsoft.eShopWeb.Web.Interfaces;
using Microsoft.eShopWeb.Web.Services;
using Microsoft.eShopWeb.Web.ViewModels;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;

namespace Microsoft.eShopWeb.Web.Pages.Admin
{
    [Authorize(Roles = AuthorizationConstants.Roles.ADMINISTRATORS)]
    public class AddCatalogItemModel : PageModel
    {
        private readonly ICatalogItemViewModelService _catalogItemViewModelService;
        private readonly CatalogNotifications _catalogNotifications;
        private readonly IAsyncRepository<CatalogItem> _catalogItemRepository;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AddCatalogItemModel(ICatalogItemViewModelService catalogItemViewModelService, CatalogNotifications catalogNotifications, IAsyncRepository<CatalogItem> catalogItemRepository, IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            _catalogItemViewModelService = catalogItemViewModelService;
            _catalogNotifications = catalogNotifications;
            _catalogItemRepository = catalogItemRepository;
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
        }

        [BindProperty]
        public CatalogItemViewModel CatalogModel { get; set; } = new CatalogItemViewModel();

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var pathImagesString = _configuration.GetValue<string>("UploadPaths:Images");

                // get last itemId
                var catalogItems = await _catalogItemRepository.ListAllAsync();
                var catalogItem = catalogItems.OrderBy(x => x.Id).LastOrDefault();
                var nextId = (catalogItem==null?0:catalogItem.Id) + 1;

                
                var extension = CatalogModel.FormImage.FileName.Substring(CatalogModel.FormImage.FileName.LastIndexOf(".")+1);
                var nameImg = $"{nextId}.{extension}";
                CatalogModel.PictureUri = $"{pathImagesString}/{nameImg}";

                var path = $"{_webHostEnvironment.WebRootPath}{pathImagesString}\\{nameImg}".Replace("/", "\\");
                var stream = new FileStream(path, FileMode.Create);

                await CatalogModel.FormImage.CopyToAsync(stream);
                
                await _catalogItemViewModelService.AddCatalogItem(CatalogModel);
            }

            return RedirectToPage("/Admin/Index");
        }
    }
}
