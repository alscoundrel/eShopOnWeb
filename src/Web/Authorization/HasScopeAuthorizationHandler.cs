using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Microsoft.eShopWeb.Web.Authorization {

    /// <summary>
    /// Has scope authorization handler
    /// </summary>
    public class HasScopeAuthorizationHandler : AuthorizationHandler<HasScopeRequirement> {
        public const string SCOPE_CLAIM_TYPE = "scope";
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HasScopeRequirement requirement) {
            // If user does not have the scope claim, get out of here
            if (!context.User.HasClaim(claim => claim.Type == SCOPE_CLAIM_TYPE && claim.Issuer == requirement.Issuer)) {
                return Task.CompletedTask;
            }

            // Split the scopes string into an array
            var scopeClaims = context.User.FindAll(claim => claim.Type == SCOPE_CLAIM_TYPE && claim.Issuer == requirement.Issuer);
            var scopes = scopeClaims.SelectMany(claim => claim.Value.Split(requirement.ScopeSeparator))
                .Where(scope => !string.IsNullOrEmpty(scope));

            // Succeed if the scope array contains the required scope
            if (scopes.Any(scope => scope == requirement.Scope)) {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
} 