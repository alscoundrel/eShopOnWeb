using System;
using Microsoft.AspNetCore.Authorization;

namespace Microsoft.eShopWeb.Web.Authorization {
    public class HasScopeRequirement : IAuthorizationRequirement {
        public const string DEFAULT_SCOPE_SEPARATOR = " ";

        public string Issuer { get; }
        public string Scope { get; }
        public string ScopeSeparator { get; }

        public HasScopeRequirement(string scope, string issuer, string scopeSeparator = null) {
            Scope = scope ??
                throw new ArgumentNullException(nameof(scope));
            Issuer = issuer ??
                throw new ArgumentNullException(nameof(issuer));
            ScopeSeparator = scopeSeparator ?? DEFAULT_SCOPE_SEPARATOR;
        }
    }
}