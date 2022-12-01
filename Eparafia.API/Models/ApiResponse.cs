namespace Eparafia.API.Models;

public class ApiResponse
{
    public int statusCode { get; private set; }
    public object data { get; private set; }
    public Dictionary<string,string[]> errors { get; private set; }

    public static ApiResponse Success(int statusCode, object data) => new(statusCode, data, null);
    public static ApiResponse Failure(int statusCode, Dictionary<string,string[]> errors) => new(statusCode, null, errors);
    public static ApiResponse Failure(int statusCode, string error) => new(statusCode, null, new Dictionary<string, string[]> { { "Message", new string[] { error } } });

    private ApiResponse(int statusCode, object data, Dictionary<string,string[]> errors)
    {
        this.statusCode = statusCode;
        this.data = data;
        this.errors = errors;
    }
}