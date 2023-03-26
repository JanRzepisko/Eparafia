using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Shared.Service.Interfaces;

namespace Shared.PublicMiddlewares;

public class UserProviderMiddleware
{
    private readonly RequestDelegate _next;

    public UserProviderMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IUserProvider userProvider)
    {
        var hasAuthorizations = context.GetEndpoint()!.Metadata.Any(c => c.GetType() == typeof(AuthorizeAttribute));
        var forAnonymous = context.GetEndpoint()!.Metadata.Any(c => c.GetType() == typeof(AllowAnonymousAttribute));
        if (!hasAuthorizations || forAnonymous)
        {
            await _next(context);
        }
        else
        {
            userProvider.SetUser(Guid.Parse(context.User.Claims.First(c => c.Type == "Id").Value));
            await _next(context);
        }
    }
}