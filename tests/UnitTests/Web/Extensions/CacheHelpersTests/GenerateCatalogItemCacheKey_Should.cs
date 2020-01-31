using System.Globalization;
using Microsoft.eShopWeb.Web;
using Microsoft.eShopWeb.Web.Extensions;
using Microsoft.eShopWeb.Web.ViewModels;
using Xunit;

namespace Microsoft.eShopWeb.UnitTests.Web.Extensions.CacheHelpersTests
{
    public class GenerateCatalogItemCacheKey_Should
    {
        [Fact]
        public void ReturnCatalogItemCacheKey()
        {
            var catalogPageFiltersViewModel = new CatalogPageFiltersViewModel();

            var result = CacheHelpers.GenerateCatalogItemCacheKey(catalogPageFiltersViewModel);

            Assert.Equal("items-0-9------", result);
        }
    }
}
