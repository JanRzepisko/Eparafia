using Microsoft.AspNetCore.Authorization;

namespace Shared.BaseModels.Jwt;

public static class JwtPolicies
{
    public const string Admin = "Admin";
    public const string User = "User";
    public const string Priest = "Priest";

    public static AuthorizationPolicy AdminPolicy()
    {
        return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(Admin).Build();
    }

    public static AuthorizationPolicy UserPolicy()
    {
        return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(User).Build();
    }

    public static AuthorizationPolicy PriestPolicy()
    {
        return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(Priest).Build();
    }
}