﻿using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.eShopWeb.Web.Interfaces;
using Microsoft.eShopWeb.Web.ViewModels;
using Microsoft.Extensions.Caching.Memory;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.Web.Pages.Pdf
{
    public class IndexPdfModel : PageModel
    {
        private readonly ICatalogViewModelService _catalogViewModelService;
        private readonly IMemoryCache _cache;

        private const string PREVIOUS_SEARCH_TEXT = "PreviousSearchText";

        public IndexPdfModel(ICatalogViewModelService catalogViewModelService, IMemoryCache cache)
        {
            _catalogViewModelService = catalogViewModelService;
            _cache = cache;
        }

        public CatalogIndexViewModel CatalogModel { get; set; } = new CatalogIndexViewModel();
        public CatalogPageFiltersViewModel CatalogPageModel {get; set;} = new CatalogPageFiltersViewModel();

        public async Task OnGet(CatalogPageFiltersViewModel catalogPageModel, string culture)//CatalogIndexViewModel catalogModel
        {   
            catalogPageModel.ItemsPerPage = 0;
            catalogPageModel.PageId = 0;
            
            if(!string.IsNullOrEmpty(culture)){
                catalogPageModel.Culture = culture;
            }
            
            CatalogModel = await _catalogViewModelService.GetCatalogItems(catalogPageModel, false, true);
            CatalogPageModel = catalogPageModel;
        }

    }
}
