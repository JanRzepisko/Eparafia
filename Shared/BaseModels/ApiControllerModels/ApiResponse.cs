using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Shared.BaseModels.ApiControllerModels;

public sealed class ApiResponse
{
    public int StatusCode { get; private set; }
    public object Data { get; private set; }
    public Dictionary<string, string[]> Errors { get; private set; }

    public static ApiResponse Success(int statusCode, object data) =>new(statusCode, data, null);
    public static ApiResponse Success(int statusCode) => new(statusCode, null, null);

    public static ApiResponse Failure(int statusCode, Dictionary<string, string[]> errors) =>new(statusCode, null, errors);

    public static ApiResponse Failure(int statusCode, string error) => new(statusCode, null,
        new Dictionary<string, string[]> { { "Message", new string[] { error } } });

    private ApiResponse(int statusCode, object data, Dictionary<string, string[]> errors)
    {
        this.StatusCode = statusCode;
        this.Data = data;
        this.Errors = errors;
    }
}