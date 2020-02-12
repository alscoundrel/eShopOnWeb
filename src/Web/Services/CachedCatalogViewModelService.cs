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

        public async Task<CatalogItemViewModel> GetItemById(int idItem, CancellationToken cancellationToken = default)
        {
            return await _catalogViewModelService.GetItemById(idItem, cancellationToken);
        }

        /// <summary>
        /// Get catalog items
        /// </summary>
        /// <param name="catalogPageFiltersViewModel"></param>
        /// <param name="useCache"></param>
        /// <param name="convertPrice"></param>
        /// <param name="cancelattionToken"></param>
        /// <returns></returns>
        public async Task<CatalogIndexViewModel> GetCatalogItems(CatalogPageFiltersViewModel catalogPageFiltersViewModel, bool useCache, bool convertPrice = false, CancellationToken cancelattionToken = default(CancellationToken))
        {
            if(useCache){
                var cacheKey = CacheHelpers.GenerateCatalogItemCacheKey(catalogPageFiltersViewModel);

                return await _cache.GetOrCreateAsync(cacheKey, async entry =>
                {
                    entry.SlidingExpiration = CacheHelpers.DefaultCacheDuration;
                    return await _catalogViewModelService.GetCatalogItems(catalogPageFiltersViewModel, useCache, convertPrice, cancelattionToken);
                });
            }
            else{
                return await _catalogViewModelService.GetCatalogItems(catalogPageFiltersViewModel, useCache, convertPrice, cancelattionToken);
            }
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
