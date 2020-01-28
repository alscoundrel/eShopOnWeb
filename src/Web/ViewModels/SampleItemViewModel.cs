using System;
using System.Collections.Generic;

namespace Microsoft.eShopWeb.Web.ViewModels
{
    public class SampleItemViewModel
    {
        public IEnumerable<CatalogItemViewModel> Items { get; set;}
        public Func<dynamic, object> Template {get;set;}
        public bool Ordered {get;set;} = true;
        public string ListClass {get;set;} = null;
        public string ListItemClass {get;set;} = null;
    }
}