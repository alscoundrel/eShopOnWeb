using System;
using Microsoft.eShopWeb.Web.ViewModels;

namespace Microsoft.eShopWeb.Web.Extensions
{
    public static class CacheHelpers
    {
        public static readonly TimeSpan DefaultCacheDuration = TimeSpan.FromSeconds(30);
        private static readonly string _itemsKeyTemplate = "items-{0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}";

        public static string GenerateCatalogItemCacheKey(CatalogPageFiltersViewModel catalogPageFiltersViewModel)
        {
            string searchTerms = string.IsNullOrEmpty(catalogPageFiltersViewModel.SearchTextFilter)?"":catalogPageFiltersViewModel.SearchTextFilter.Replace(" ", "");
            return string.Format(_itemsKeyTemplate, catalogPageFiltersViewModel.PageId, catalogPageFiltersViewModel.ItemsPerPage, catalogPageFiltersViewModel.Culture, catalogPageFiltersViewModel.BrandFilter, catalogPageFiltersViewModel.TypesFilter, searchTerms, 
            catalogPageFiltersViewModel.OrderBy, catalogPageFiltersViewModel.Ordination);
        }

        public static string GenerateBrandsCacheKey()
        {
            return "brands";
        }

        public static string GenerateTypesCacheKey()
        {
            return "types";
        }
    }
}
