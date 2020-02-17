using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using System.Linq;

namespace Microsoft.eShopWeb.Web.Swagger
{
    public class AssignOAuth2SecurityRequirements : IOperationFilter
    {

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            // Map each "Authorize" policy to an oauth2 scope
            var controllerType = context.MethodInfo.DeclaringType;
            var methodScopes = context.MethodInfo.GetCustomAttributes()
                .OfType<AuthorizeAttribute>()
                .Select(attr => attr.Policy)
                .Distinct();
            var controllerScopes = controllerType.GetCustomAttributes()
                .OfType<AuthorizeAttribute>()
                .Select(attr => attr.Policy)
                .Distinct();
            var scopes = new [] { "catalog-item:read"}; // methodScopes.Union(controllerScopes).ToList();

            if (scopes.Any())
            {
                if (operation.Security == null) {
                    operation.Security = new List<OpenApiSecurityRequirement>();
                }

                var openApiSecurityScheme = new OpenApiSecurityScheme {
                    Reference = new OpenApiReference {
                        Type = ReferenceType.SecurityScheme,
                        Id = SwaggerConstants.IDENTITY_SERVER_SECURITY_SCHEME.Name }
                };
                var oAuthRequirements = new OpenApiSecurityRequirement
                {
                    { openApiSecurityScheme, scopes }
                };
                operation.Security.Add(oAuthRequirements);
            }
        }
    }
} 