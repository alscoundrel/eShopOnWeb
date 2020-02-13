using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.eShopWeb.Web.Pages.Store.Availability
{
    public class ItemsAvailabilityViewModel
    {
        public int StoreId { get; set; }
        public decimal Amount { get; set; }
        public string Unit { get; set;}
    }
}
