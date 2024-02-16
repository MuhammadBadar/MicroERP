using Microsoft.AspNetCore.Authorization;

namespace QST.MicroERP.Core.Entities.SEC
{
    public class AuthorizePolicyRequirement : IAuthorizationRequirement
    {
        public AuthorizePolicyRequirement(params IAuthorizePolicy[] requiredPolicies)
        {
            RequiredPolicies = requiredPolicies;
        }

        public IAuthorizePolicy[] RequiredPolicies { get; }
    }
}
