using Eparafia.Infrastructure.Exceptions;
using FluentValidation.Results;

namespace Eparafia.Application.Exceptions;

public sealed class InvalidRequestException : BaseException
{
    public override int StatusCodeToRise => 400;
    public InvalidRequestException(string message) : base(message) { }
    public InvalidRequestException(Dictionary<string, string[]> errors) : base(errors) { }
}