using ApplicationCore.UseTypes;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.eShopWeb.Web.Extensions;
using Microsoft.eShopWeb.Web.Services;
using Microsoft.eShopWeb.Web.ViewModels;
using Microsoft.Extensions.Caching.Memory;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.Web.Pages.Pdf
{
    public class IndexPdfModel : PageModel
    {
        private readonly ICatalogViewModelService _catalogViewModelService;
        private readonly IMemoryCache _cache;

        private const string PREVIOUS_SEARCH_TEXT = "PreviousSearchText";

        public IndexPdfModel(ICatalogViewModelService catalogViewModelService, IMemoryCache cache)
        {
            _catalogViewModelService = catalogViewModelService;
            _cache = cache;
        }

        public CatalogIndexViewModel CatalogModel { get; set; } = new CatalogIndexViewModel();
        public CatalogPageFiltersViewModel CatalogPageModel {get; set;} = new CatalogPageFiltersViewModel();

        public async Task OnGet(CatalogPageFiltersViewModel catalogPageModel, bool icf, string culture)//CatalogIndexViewModel catalogModel
        {   
            catalogPageModel.ItemsPerPage = 0;
            catalogPageModel.PageId = 0;
            
            if(!string.IsNullOrEmpty(culture)){
                catalogPageModel.Culture = culture;
            }
            
            CatalogModel = await _catalogViewModelService.GetCatalogItems(catalogPageModel, true);
            CatalogPageModel = catalogPageModel;
        }

    }
}
