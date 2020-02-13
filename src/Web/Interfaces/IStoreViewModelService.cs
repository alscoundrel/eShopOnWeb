using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.eShopWeb.Web.Pages.Store.Availability;

namespace Microsoft.eShopWeb.Web.Interfaces
{
    public interface IStoreViewModelService
    {
        Task<StoreItemsAvailabilityViewModel> GetItemAvailabilityByStore(int CatalogItemId, CancellationToken cancelationToken = default(CancellationToken));
        Task<IEnumerable<SelectListItem>> GetStores(CancellationToken cancelationToken = default(CancellationToken));
    }
}
