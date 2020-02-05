using System.Collections.Generic;
using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Localization.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Web.Extensions.Middleware
{
    public static class RequestCultureMiddlewareExtensions
    {
        public static void UseRequestCulture(this IApplicationBuilder app)
        {
            var supportedCultures = new[]
            {
                new CultureInfo("en-US"),
                new CultureInfo("en-GB"),
                new CultureInfo("de"),
                new CultureInfo("fr-FR"),
                new CultureInfo("pt-PT")
            };
            var configuration = app.ApplicationServices.GetService<IConfiguration>();
            var defaulUserCulture = configuration.GetValue<string>("DefaultCulture") ?? "pt-PT";
            var options = new RequestLocalizationOptions()
            {
                DefaultRequestCulture = new RequestCulture(culture: defaulUserCulture, uiCulture: defaulUserCulture),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            };
            options.RequestCultureProviders = new List<IRequestCultureProvider>()  
            {
                new QueryStringRequestCultureProvider(),
                new CookieRequestCultureProvider(),
                new RouteDataRequestCultureProvider() { Options = options } 
            };
            options.RequestCultureProviders.Insert(0, new RouteDataRequestCultureProvider());
            app.UseRequestLocalization(options);
            
        }
    }
}