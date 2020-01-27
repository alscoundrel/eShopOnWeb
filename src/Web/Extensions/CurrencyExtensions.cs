using ApplicationCore.Interfaces;
using Infrastructure.Services;
using Microsoft.eShopWeb;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Services;
using Microsoft.eShopWeb.Infrastructure.Data;
using Microsoft.eShopWeb.Web.Interfaces;
using Microsoft.eShopWeb.Web.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Web.Extensions
{
    public static class CurrencyExtensions
    {
        /// <summary>
        /// Add catalog services
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddCurrencyServices(this IServiceCollection services, IConfiguration configuration) {
            services.AddScoped<ICurrencyRatesService, CachedCurrencyRatesServiceExchangeRates>();
            services.AddScoped<CurrencyRatesServiceExchangeRates>();
        }

    }
} 