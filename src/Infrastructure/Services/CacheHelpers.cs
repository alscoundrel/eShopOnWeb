using System;

namespace Infrastructure.Services
{
    public static class CacheHelpers
    {
        public static readonly TimeSpan DefaultCacheDuration = TimeSpan.FromSeconds(300);

        public static string GenerateExchangeRatesCacheKey(string currency){
            return string.Format("exchangerats-{0}", currency);
        }
    }
}
