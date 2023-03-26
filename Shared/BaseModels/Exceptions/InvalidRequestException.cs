namespace Shared.BaseModels.Exceptions;

public sealed class InvalidRequestException : BaseException
{
    public InvalidRequestException(string message) : base(message)
    {
    }

    public InvalidRequestException(Dictionary<string, string[]> errors) : base(errors)
    {
    }

    public override int StatusCodeToRise => 400;
}