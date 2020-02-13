using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.eShopWeb.Web.Pages.Store.Availability
{
    public class ItemsAvailabilityByStoreViewModel
    {
        public int StoreId { get; set; }
        public decimal Amount { get; set; }
        public string Unit { get; set; }
        public string StoreName { get; set; }
    }
}
