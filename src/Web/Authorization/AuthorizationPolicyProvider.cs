using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Microsoft.eShopWeb.Web.Authorization {

    /// <summary>
    /// Custom authorization policy provider (aka Authorization policy factory)
    /// </summary>
    public class AuthorizationPolicyProvider : DefaultAuthorizationPolicyProvider {
        private readonly AuthorizationOptions _options;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="options">Authorization options</param>
        /// <param name="configuration">Configuration</param>
        public AuthorizationPolicyProvider(IOptions<AuthorizationOptions> options, IConfiguration configuration) : base(options) {
            _options = options.Value;
            _configuration = configuration;
        }

        /// <inheritdoc />
        public override async Task<AuthorizationPolicy> GetPolicyAsync(string policyName) {
            // Check static policies first
            var policy = await base.GetPolicyAsync(policyName);

            if (policy == null) {
                policy = new AuthorizationPolicyBuilder()
                    .AddRequirements(new HasScopeRequirement(policyName, "http://localhost:7000"))
                    .Build();

                // Add policy to the AuthorizationOptions, so we don't have to re-create it each time
                _options.AddPolicy(policyName, policy);
            }

            return policy;
        }
    }
} 