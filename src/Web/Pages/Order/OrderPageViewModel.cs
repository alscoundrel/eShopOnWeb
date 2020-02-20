using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationCore.UseTypes;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Microsoft.eShopWeb.Web.Pages.Order
{
    public class OrderPageViewModel
    {
        public int Id { get; set; }
        public OrderStatus Status { get; set; }
        public string Comment { get; set; }
        public IEnumerable<SelectListItem> StatusList { get; set; }
    }
}
