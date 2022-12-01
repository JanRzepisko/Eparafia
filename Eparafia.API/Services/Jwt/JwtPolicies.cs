using Microsoft.AspNetCore.Authorization;

namespace Eparafia.API.Services.Jwt;

public static class JwtPolicies
{
    public const string Admin = "Admin";
    public const string User = "User";
        
    public static AuthorizationPolicy AdminPolicy()
    {
        return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(Admin).Build();
    }
    public static AuthorizationPolicy UserPolicy()
    {
        return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(User).Build();
    }
}