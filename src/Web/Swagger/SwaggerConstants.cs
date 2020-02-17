using System;
using System.Collections.Generic;
using Microsoft.OpenApi.Models;

namespace Microsoft.eShopWeb.Web.Swagger
{
    internal static class SwaggerConstants
    {
        public static OpenApiSecurityScheme IDENTITY_SERVER_SECURITY_SCHEME = new OpenApiSecurityScheme
        {
            Name = "identity-server",
            Type = SecuritySchemeType.OAuth2,
            Description = "Identity server authentication server",
            In = ParameterLocation.Header,
            OpenIdConnectUrl = new Uri("http://localhost:7000/.well-known/configuration", UriKind.Absolute),
            Scheme = "Bearer",
            Flows = new OpenApiOAuthFlows
            {
                Implicit = new OpenApiOAuthFlow
                {
                    AuthorizationUrl = new Uri("http://localhost:7000/connect/authorize", UriKind.Absolute),
                    Scopes = new Dictionary<string, string>
                    {
                        // { "profile", "View user profile info" },
                        // { "openid", "View user openID info" },
                        // { "email", "View user's email" },

                        { "order:read", "View orders" },
                        { "order:write", "Manage orders" },

                        { "catalog-item:read", "View catalog items" },
                        { "catalog-item:write", "Manage catalog items" },

                        { "catalog-brand:read", "View brands" },
                        { "catalog-brand:write", "Manage brands" },

                        { "catalog-type:read", "View item types" },
                        { "catalog-type:write", "Manage item types" }
                    }
                }
            }
        };

    }
} 