using Microsoft.eShopWeb.Web.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.eShopWeb.Web.ViewModels;
using Microsoft.eShopWeb.Web.Interfaces;

namespace Microsoft.eShopWeb.Web.Controllers.Api
{
    public class CatalogController : BaseApiController
    {
        private readonly ICatalogViewModelService _catalogViewModelService;

        public CatalogController(ICatalogViewModelService catalogViewModelService) => _catalogViewModelService = catalogViewModelService;

        [HttpGet]
        public async Task<ActionResult<CatalogViewModelService>> List(int? brandFilterApplied, int? typesFilterApplied, int? page, string searchText = null)
        {   
            var catalogPageFiltersViewModel = new CatalogPageFiltersViewModel(){
                PageId = page??0,
                ItemsPerPage = 9,
                BrandFilter = brandFilterApplied,
                TypesFilter = typesFilterApplied,
                SearchTextFilter = searchText
            };          
            var catalogModel = await _catalogViewModelService.GetCatalogItems(catalogPageFiltersViewModel, false, true);
            return Ok(catalogModel);
        }
        
        [HttpGet("{idItem}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult<CatalogItemViewModel>> GetById(int idItem) {
            try  {
                var catalogItem = await _catalogViewModelService.GetItemById(idItem);
                return Ok(catalogItem);
            } catch (ModelNotFoundException) {
                return NotFound();
            }
        }
    }
}
