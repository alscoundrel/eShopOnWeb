using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.eShopWeb.Web.Services;
using Microsoft.eShopWeb.Web.ViewModels;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ICatalogViewModelService _catalogViewModelService;

        public IndexModel(ICatalogViewModelService catalogViewModelService)
        {
            _catalogViewModelService = catalogViewModelService;
        }

        public CatalogIndexViewModel CatalogModel { get; set; } = new CatalogIndexViewModel();
        public CatalogPageFiltersViewModel CatalogPageModel {get; set;} = new CatalogPageFiltersViewModel();

        public async Task OnGet(CatalogPageFiltersViewModel catalogPageModel, int? pageId)//CatalogIndexViewModel catalogModel
        {
            /*
            var catalogPageFiltersViewModel = new CatalogPageFiltersViewModel(){
                PageId = pageId??0,
                ItemsPerPage = Constants.ITEMS_PER_PAGE,
                BrandFilter = catalogModel.BrandFilterApplied,
                TypesFilter = catalogModel.TypesFilterApplied,
                SearchTextFilter = catalogModel.SearchTextFilter,
                OrderBy = catalogModel.OrderBy,
                Ordination = catalogModel.Ordination
            };
            */
            CatalogModel = await _catalogViewModelService.GetCatalogItems(catalogPageModel, true);
            CatalogPageModel = catalogPageModel;
        }
    }
}
