using FluentValidation;
using MediatR;
using Shared.BaseModels.Exceptions;

namespace Shared.Behaviours;

public sealed class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _Validators;

    public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
    {
        _Validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (_Validators.Any())
        {
            var validationContext = new ValidationContext<TRequest>(request);
            var validationResults =
                await Task.WhenAll(_Validators.Select(c => c.ValidateAsync(validationContext, cancellationToken)));
            if (validationResults.Length > 0 && validationResults.Any(c => !c.IsValid))
                throw new InvalidRequestException(string.Join("; ", validationResults.SelectMany(c => c.Errors)));
        }

        return await next();
    }
}