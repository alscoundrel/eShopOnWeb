using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Localization.Routing;
using Web.Middleware;

namespace Web.Extensions.Middleware
{
    public static class RequestCultureMiddlewareExtensions
    {
        public static void UseRequestCulture(this IApplicationBuilder app)
        {
            app.UseMiddleware<RequestCultureMiddleware>();

            var supportedCultures = new[]
            {
                new CultureInfo("en-US"),
                new CultureInfo("en-GB"),
                new CultureInfo("de"),
                new CultureInfo("fr-FR"),
                new CultureInfo("pt-PT")
            };

<<<<<<< HEAD
            var defaulCulture = "pt-PT";
=======
            var defaulCulture = "en-GB";
>>>>>>> 7593090aa30397e7f73b49dbc228a9cf3acf745c
            var options = new RequestLocalizationOptions()
            {
                DefaultRequestCulture = new RequestCulture(culture: defaulCulture, uiCulture: defaulCulture),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            };
            options.RequestCultureProviders = new[] 
            { 
                new RouteDataRequestCultureProvider() { Options = options } 
            };
            app.UseRequestLocalization(options);
        }
    }
}