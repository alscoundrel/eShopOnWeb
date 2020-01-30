using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.eShopWeb.Web.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace Microsoft.eShopWeb.Web.Services
{
    public interface ICatalogViewModelService
    {
        Task<CatalogIndexViewModel> GetCatalogItems(CatalogPageFiltersViewModel catalogPageFiltersViewModel, bool convertPrice = false, CancellationToken cancelationToken = default(CancellationToken));
        Task<IEnumerable<SelectListItem>> GetBrands(CancellationToken cancelationToken = default(CancellationToken));
        Task<IEnumerable<SelectListItem>> GetTypes(CancellationToken cancelationToken = default(CancellationToken));

        /// <summary>
        /// Get Catalog Item By Id
        /// </summary>
        /// <param name="idItem"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<CatalogItemViewModel> GetItemById(int idItem, CancellationToken cancellationToken = default(CancellationToken));
    }
}
