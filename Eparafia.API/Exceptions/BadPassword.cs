namespace Eparafia.API.Exceptions;

public sealed class BadPassword : BaseException
{
    public override int StatusCodeToRise => 403;
    public BadPassword(string message) : base(message) { }
    public BadPassword(Dictionary<string, string[]> errors) : base(errors) { }
}