using System;
using System.Collections.Generic;
using Microsoft.eShopWeb.Web.Pages.WishList;

namespace Microsoft.eShopWeb.Web.ViewModels
{
    public class SampleWishListItemViewModel
    {
        public IEnumerable<WishListItemViewModel> Items { get; set;}
        public Func<dynamic, object> Template {get;set;}
        public bool Ordered {get;set;} = true;
        public string ListClass {get;set;} = null;
        public string ListItemClass {get;set;} = null;
    }
}