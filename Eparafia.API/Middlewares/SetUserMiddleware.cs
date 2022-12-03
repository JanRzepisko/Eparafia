using Eparafia.Application.Services.UserProvider;
using Microsoft.AspNetCore.Authorization;

namespace Eparafia.API.Middlewares;

public class SetUserMiddleware
{
    private readonly RequestDelegate _next;
    
    public SetUserMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IUserProvider userProvider)
    {
        bool hasAuthorizations = context.GetEndpoint()!.Metadata.Any(c => c.GetType() == typeof(AuthorizeAttribute));
        if (!hasAuthorizations)
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