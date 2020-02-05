using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using ApplicationCore.UseTypes;

namespace Microsoft.eShopWeb.Web.ViewModels
{
    public class CatalogPageFiltersViewModel
    {
        public int PageId {get; set;} = 0;
        public int ItemsPerPage { get; set;} = Constants.ITEMS_PER_PAGE;
        public int? BrandFilter { get; set; }
        public int? TypesFilter { get; set; }
        public string SearchTextFilter {get; set;}
        public NamesOrderBy? OrderBy {get;set;}
        public Ordination Order{get;set;}
        public ViewsModes ViewMode { get; set;}
        public string Culture { get; set; }
    }
}
