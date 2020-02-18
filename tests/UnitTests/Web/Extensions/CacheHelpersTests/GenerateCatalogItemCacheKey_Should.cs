using System;
using ApplicationCore.UseTypes;
using Microsoft.eShopWeb.Web;
using Microsoft.eShopWeb.Web.Extensions;
using Microsoft.eShopWeb.Web.ViewModels;
using Xunit;

namespace Microsoft.eShopWeb.UnitTests.Web.Extensions.CacheHelpersTests
{
    public class GenerateCatalogItemCacheKey_Should
    {
        [Theory]
        [InlineData(0, Constants.ITEMS_PER_PAGE, null, null, null, null, null, Ordination.ASC, "items-0-9------ASC")]
        [InlineData(5, 20, null, null, null, null, null, Ordination.ASC, "items-5-20------ASC")]
        [InlineData(5, 20, "pt", null, null, null, null, Ordination.ASC, "items-5-20-pt-----ASC")]
        [InlineData(5, 20, null, null, null, null, NamesOrderBy.Name, Ordination.DESC, "items-5-20-----Name-DESC")]
        [InlineData(5, 20, "en", 1, 2, "Search", NamesOrderBy.Name, Ordination.DESC, "items-5-20-en-1-2-Search-Name-DESC")]
        [InlineData(-5, 20, null, null, null, null, null, Ordination.ASC, null, typeof(InvalidPageIndexException))]
        public void ReturnCatalogItemCacheKey(
            int pageIndex,
            int itemPerPage,
            string culture,
            int? brandId,
            int? typeId,
            string searchText,
            NamesOrderBy? orderBy,
            Ordination order,
            string expectedResult,
            Type exceptionType = null
        )
        {
            var catalogPageFiltersViewModel = new CatalogPageFiltersViewModel(){
                PageId = pageIndex,
                ItemsPerPage = itemPerPage,
                Culture = culture,
                BrandFilter = brandId,
                TypesFilter =typeId,
                SearchTextFilter = searchText,
                OrderBy = orderBy,
                Order = order
            };

            if (string.IsNullOrEmpty(expectedResult)) {
                if (exceptionType == null) {
                    throw new Exception("Missing exception type to check");
                }
                Assert.Throws(
                    exceptionType,
                    () => CacheHelpers.GenerateCatalogItemCacheKey(catalogPageFiltersViewModel));
            }
            else 
            {
                var result = CacheHelpers.GenerateCatalogItemCacheKey(catalogPageFiltersViewModel);

                Assert.Equal(expectedResult, result);
            }
        }
        
    }
}
