using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Shared.BaseModels.ApiControllerModels;
using Shared.BaseModels.Exceptions;

namespace Shared.PublicMiddlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (BaseException baseAppException)
        {
            await ThrowError(context, baseAppException.StatusCodeToRise, baseAppException.Errors);
        }
        catch (Exception exception)
        {
            await ThrowError(context, 500, new Dictionary<string, string[]> { { "Message", new[] { exception.Message } } });
        }
    }

    private static Task ThrowError(HttpContext context, int statusCode, Dictionary<string, string[]> errors)
    {
        var response = context.Response;
        response.ContentType = "application/json";
        response.StatusCode = statusCode;
        return response.WriteAsync(JsonConvert.SerializeObject(ApiResponse.Failure(statusCode, errors)));
    }
}