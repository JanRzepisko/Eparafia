namespace Shared.BaseModels.Exceptions;

public sealed class BadPassword : BaseException
{
    public BadPassword(string message) : base(message)
    {
    }

    public BadPassword(Dictionary<string, string[]> errors) : base(errors)
    {
    }

    public override int StatusCodeToRise => 403;
}