using Eparafia.Application.DataAccess;
using Eparafia.Application.Services.UserProvider;
using Eparafia.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;

namespace Eparafia.API.Middlewares;

public class SetUserMiddleware
{
    private readonly RequestDelegate _next;
    
    public SetUserMiddleware(RequestDelegate next, IUnitOfWork unitOfWork)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IUserProvider userProvider)
    {
        Console.WriteLine(JsonConvert.SerializeObject(context.GetEndpoint()));
        bool hasAuthorizations = context.GetEndpoint()!.Metadata.Any(c => c.GetType() == typeof(AuthorizeAttribute));
        bool forAnonymous = context.GetEndpoint()!.Metadata.Any(c => c.GetType() == typeof(AllowAnonymousAttribute));
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