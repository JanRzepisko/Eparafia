namespace Shared.BaseModels.ApiControllerModels;

public sealed class ApiResponse
{
    private ApiResponse(int statusCode, object data, Dictionary<string, string[]> errors)
    {
        StatusCode = statusCode;
        Data = data;
        Errors = errors;
    }

    public int StatusCode { get; }
    public object Data { get; }
    public Dictionary<string, string[]> Errors { get; }

    public static ApiResponse Success(int statusCode, object data)
    {
        return new(statusCode, data, null);
    }

    public static ApiResponse Success(int statusCode)
    {
        return new(statusCode, null, null);
    }

    public static ApiResponse Failure(int statusCode, Dictionary<string, string[]> errors)
    {
        return new(statusCode, null, errors);
    }

    public static ApiResponse Failure(int statusCode, string error)
    {
        return new(statusCode, null,
            new Dictionary<string, string[]> { { "Message", new[] { error } } });
    }
}