namespace Shared.BaseModels.Exceptions;

public abstract class BaseException : Exception
{
    public BaseException(string message) : base(message)
    {
        Errors = new Dictionary<string, string[]> { { "Message", new[] { message } } };
    }

    public BaseException(Dictionary<string, string[]> errors)
    {
        Errors = errors;
    }

    public abstract int StatusCodeToRise { get; }
    public Dictionary<string, string[]> Errors { get; init; }
}