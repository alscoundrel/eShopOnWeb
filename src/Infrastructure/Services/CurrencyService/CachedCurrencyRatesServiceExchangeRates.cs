using System.Threading.Tasks;
using System.Threading;
using Microsoft.Extensions.Caching.Memory;
using ApplicationCore.Interfaces;

namespace Infrastructure.Services
{
    public class CachedCurrencyRatesServiceExchangeRates : ICurrencyRatesService
    {
        private readonly IMemoryCache _cache;
        private readonly CurrencyRatesServiceExchangeRates _currencyRatesServiceExchangeRates;

        public CachedCurrencyRatesServiceExchangeRates(IMemoryCache cache, CurrencyRatesServiceExchangeRates currencyRatesServiceExchangeRates){
            _cache = cache;
            _currencyRatesServiceExchangeRates = currencyRatesServiceExchangeRates;
        }

        public async Task<string> Request(ICurrencyService.Currency source, CancellationToken cancellationToken = default)
        {
                return await _cache.GetOrCreateAsync(CacheHelpers.GenerateExchangeRatesCacheKey(), async entry =>
                {
                    entry.SlidingExpiration = CacheHelpers.DefaultCacheDuration;
                    return await _currencyRatesServiceExchangeRates.Request(source, cancellationToken);
                });
        }
    }
}
