using Microsoft.eShopWeb.Web.Interfaces;
using Microsoft.eShopWeb.Web.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Web.Extensions
{
    public static class ViewsExtensions
    {
        /// <summary>
        /// Add catalog services
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddViewsServices(this IServiceCollection services, IConfiguration configuration) {
            services.AddScoped<IViewRenderService, ViewRenderService>();
        }

    }
} 