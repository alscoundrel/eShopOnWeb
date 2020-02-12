using ApplicationCore.UseTypes;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.eShopWeb.Web.Extensions;
using Microsoft.eShopWeb.Web.Services;
using Microsoft.eShopWeb.Web.ViewModels;
using Microsoft.Extensions.Caching.Memory;
using System.Linq;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="catalogPageModel">User catalog filters</param>
        /// <param name="icf">if changed any filters</param>
        /// <param name="culture">parse culture information</param>
        /// <returns></returns>
        public async Task OnGet(CatalogPageFiltersViewModel catalogPageModel, bool icf, string culture)//CatalogIndexViewModel catalogModel
        {
            // Para o caso de o pedido de busca por termo estiver fora da pagina inicial
            if(0 < catalogPageModel.PageId && icf){
                catalogPageModel.PageId = 0;
            }
            if(!string.IsNullOrEmpty(culture)){
                catalogPageModel.Culture = culture;
            }
            
            CatalogModel = await _catalogViewModelService.GetCatalogItems(catalogPageModel, true, true);
            CatalogPageModel = catalogPageModel;

            CatalogModel.OrdersBy = Enum<NamesOrderBy>.GetAll().Select(orderBy => new SelectListItem { Value = orderBy.ToString(), Text = orderBy.ToString() });
            CatalogModel.Orders = Enum<Ordination>.GetAll().Select(order => new SelectListItem { Value = order.ToString(), Text = order.ToString() });
        }

    }
}
