using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.eShopWeb.ApplicationCore.Constants;
using Microsoft.eShopWeb.Web.Services;
using Microsoft.eShopWeb.Web.ViewModels;
using Microsoft.Extensions.Caching.Memory;
using System.Threading.Tasks;
using Microsoft.eShopWeb.Web.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.eShopWeb.ApplicationCore.Entities;

namespace Microsoft.eShopWeb.Web.Pages.Admin
{
    [Authorize(Roles = AuthorizationConstants.Roles.ADMINISTRATORS)]
    public class IndexModel : PageModel
    {
        private readonly ICatalogViewModelService _catalogViewModelService;
        private readonly ICatalogItemViewModelService _catalogItemViewModelService;
        private readonly IMemoryCache _cache;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<CatalogItem> _logger;

        public IndexModel(ICatalogViewModelService catalogViewModelService, ICatalogItemViewModelService catalogItemViewModelService, IMemoryCache cache, IConfiguration configuration, IWebHostEnvironment webHostEnvironment, ILoggerFactory loggerFactory)
        {
            _catalogViewModelService = catalogViewModelService;
            _catalogItemViewModelService = catalogItemViewModelService;
            _cache = cache;
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
            _logger = loggerFactory.CreateLogger<CatalogItem>();
        }

        public CatalogIndexViewModel CatalogModel { get; set; } = new CatalogIndexViewModel();
        public CatalogPageFiltersViewModel CatalogPageModel {get; set;} = new CatalogPageFiltersViewModel();

        public async Task OnGet(CatalogPageFiltersViewModel catalogPageModel, int? pageId)
        {
            CatalogModel = await _catalogViewModelService.GetCatalogItems(catalogPageModel, false, false);
            CatalogPageModel = catalogPageModel;
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id, string imageUri){
            _logger.LogInformation($"Delete CatalogItem with Id {id}");
            await _catalogItemViewModelService.DeleteCatalogItem(id);

            // delete the image file
            try{
                var pathImagesString = _configuration.GetValue<string>("UploadPaths:Images");
                var path = $"{_webHostEnvironment.WebRootPath}{imageUri}".Replace("/", "\\");
                System.IO.File.Delete(path);
            }
            catch(System.IO.IOException ioe){
                _logger.LogWarning($"CatalogItem Image file has not been deleted, with id {id}. Error: {ioe.Message}");
                throw new System.IO.IOException(ioe.Message, ioe.InnerException);
            }
            return RedirectToPage();
        }
    }
}
