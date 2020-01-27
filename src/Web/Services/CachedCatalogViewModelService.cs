using System.Globalization;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.eShopWeb.Web.ViewModels;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.eShopWeb.Web.Extensions;

namespace Microsoft.eShopWeb.Web.Services
{
    public class CachedCatalogViewModelService : ICatalogViewModelService
    {
        private readonly IMemoryCache _cache;
        private readonly CatalogViewModelService _catalogViewModelService;

        public CachedCatalogViewModelService(IMemoryCache cache,
            CatalogViewModelService catalogViewModelService)
        {
            _cache = cache;
            _catalogViewModelService = catalogViewModelService;
        }

        public async Task<IEnumerable<SelectListItem>> GetBrands(CancellationToken cancelattionToken = default(CancellationToken))
        {
            return await _cache.GetOrCreateAsync(CacheHelpers.GenerateBrandsCacheKey(), async entry =>
                    {
                        entry.SlidingExpiration = CacheHelpers.DefaultCacheDuration;
                        return await _catalogViewModelService.GetBrands(cancelattionToken);
                    });
        }

        public async Task<CatalogIndexViewModel> GetCatalogItems(int pageIndex, int itemsPage, int? brandId, int? typeId, CancellationToken cancelattionToken = default(CancellationToken))
        {
            var culture = CultureInfo.CurrentCulture.Name;
            var cacheKey = CacheHelpers.GenerateCatalogItemCacheKey(pageIndex, Constants.ITEMS_PER_PAGE, culture, brandId, typeId);

            return await _cache.GetOrCreateAsync(cacheKey, async entry =>
            {
                entry.SlidingExpiration = CacheHelpers.DefaultCacheDuration;
                return await _catalogViewModelService.GetCatalogItems(pageIndex, itemsPage, brandId, typeId, cancelattionToken);
            });
        }

        public async Task<IEnumerable<SelectListItem>> GetTypes(CancellationToken cancelattionToken = default(CancellationToken))
        {
            return await _cache.GetOrCreateAsync(CacheHelpers.GenerateTypesCacheKey(), async entry =>
            {
                entry.SlidingExpiration = CacheHelpers.DefaultCacheDuration;
                return await _catalogViewModelService.GetTypes(cancelattionToken);
            });
        }
    }
}
