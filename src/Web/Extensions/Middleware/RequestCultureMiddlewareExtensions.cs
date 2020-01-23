using Microsoft.AspNetCore.Builder;
using Web.Middleware;

namespace Web.Extensions.Middleware
{
    public static class RequestCultureMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestCulture(
            this IApplicationBuilder app)
        {
            return app.UseMiddleware<RequestCultureMiddleware>();
        }
    }
}