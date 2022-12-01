using Eparafia.API.Exceptions;
using Eparafia.API.Models;
using Newtonsoft.Json;

namespace Eparafia.API.Middlewares;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    public ExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch(BaseException baseAppException)
        {
            await ThrowError(context, baseAppException.StatusCodeToRise, baseAppException.Errors);
        }
        catch(Exception exception)
        {
            await ThrowError(context, 500, new Dictionary<string, string[]> { { "Message", new string[] { exception.Message } } });
        }
    }

    private Task ThrowError(HttpContext context, int statusCode, Dictionary<string,string[]> errors)
    {
        var response = context.Response;
        response.ContentType = "application/json";
        response.StatusCode = statusCode;
        return response.WriteAsync(JsonConvert.SerializeObject(ApiResponse.Failure(statusCode,errors)));
    }
}