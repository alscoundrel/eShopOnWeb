using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.eShopWeb.Web.Services;
using Microsoft.eShopWeb.Web.ViewModels;
using Microsoft.Extensions.Caching.Memory;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ICatalogViewModelService _catalogViewModelService;
        private readonly IMemoryCache _cache;

        private const string PREVIOUS_SEARCH_TEXT = "PreviousSearchText";

        public IndexModel(ICatalogViewModelService catalogViewModelService, IMemoryCache cache)
        {
            _catalogViewModelService = catalogViewModelService;
            _cache = cache;
        }

        public CatalogIndexViewModel CatalogModel { get; set; } = new CatalogIndexViewModel();
        public CatalogPageFiltersViewModel CatalogPageModel {get; set;} = new CatalogPageFiltersViewModel();

        public async Task OnGet(CatalogPageFiltersViewModel catalogPageModel, int? pageId)//CatalogIndexViewModel catalogModel
        {
            if(0 < catalogPageModel.PageId && ChangedSearchTextAsync(catalogPageModel.SearchTextFilter)){
                catalogPageModel.PageId = 0;
            }
            
            CatalogModel = await _catalogViewModelService.GetCatalogItems(catalogPageModel, true);
            CatalogPageModel = catalogPageModel;
        }

        private bool ChangedSearchTextAsync(string searchText){
            var previous = searchText;
            if(_cache.TryGetValue<string>(PREVIOUS_SEARCH_TEXT, out previous)){
                if(previous == null && searchText == null){ return false;}
                if(previous.Equals(searchText, System.StringComparison.CurrentCultureIgnoreCase)){ return false;}
            }
            else{
                _cache.CreateEntry(PREVIOUS_SEARCH_TEXT);
            }
            _cache.Set(PREVIOUS_SEARCH_TEXT, searchText);
            return true;
        }
    }
}
