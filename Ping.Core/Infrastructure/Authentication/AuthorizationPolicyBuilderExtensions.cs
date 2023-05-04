using Microsoft.AspNetCore.Authorization;

namespace Ping.Core.Infrastructure.Authentication;

public static class AuthorizationPolicyBuilderExtensions
{
    public static void RequireUserIdClaim(this AuthorizationPolicyBuilder builder)
    {
        builder.AddRequirements(new UserIdRequirement());
    } 
}