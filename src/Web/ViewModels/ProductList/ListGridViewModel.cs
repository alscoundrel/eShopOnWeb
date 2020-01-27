using System.Collections.Generic;
using Microsoft.eShopWeb.Web.ViewModels;

namespace Web.ViewModels.ProductList
{
    public class ListGridViewModel
    {
        public int ItemsNumberPage { get; set;}
        public List<CatalogItemViewModel> ListItems{ get; set;}
    }
}