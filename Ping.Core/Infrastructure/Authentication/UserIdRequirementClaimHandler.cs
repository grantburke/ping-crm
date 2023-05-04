using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace Ping.Core.Infrastructure.Authentication;

public class UserIdRequirement : IAuthorizationRequirement
{ }

public class UserIdRequirementClaimHandler : AuthorizationHandler<UserIdRequirement>
{
    private readonly HttpContext _httpContext;

    public UserIdRequirementClaimHandler(IHttpContextAccessor httpContextAccessor)
    {
        _httpContext = httpContextAccessor.HttpContext;
    }

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UserIdRequirement userIdRequirement)
    {
        var userId = _httpContext.Request.RouteValues[PolicyConstants.UserId]?.ToString();

        // Check if the user has a "Owner" role claim
        if (context.User.HasClaim(c => c.Type == ClaimTypes.Role && c.Value == PolicyConstants.Owner))
        {
            context.Succeed(userIdRequirement);
            return Task.CompletedTask;
        }

        // Check if the user has a role claim of ID that matches the ID in the request route
        if (!string.IsNullOrEmpty(userId) && context.User.HasClaim(c => c.Type == PolicyConstants.UserId && c.Value == userId))
        {
            context.Succeed(userIdRequirement);
            return Task.CompletedTask;
        }

        // If neither condition is met, the user is not authorized
        context.Fail();
        return Task.CompletedTask;
    }
}

