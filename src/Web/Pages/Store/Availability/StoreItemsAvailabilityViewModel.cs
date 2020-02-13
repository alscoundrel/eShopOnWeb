using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.eShopWeb.Web.Pages.Store.Availability
{
    public class StoreItemsAvailabilityViewModel
    {
        public int CatalogItemId { get; set; }
        public string Name { get; set; }

        public List<ItemsAvailabilityViewModel> ItemsAvailabilityViewModels { get; set; }
    }
}
