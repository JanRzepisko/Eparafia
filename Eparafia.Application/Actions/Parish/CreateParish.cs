using Eparafia.Application.DataAccess;
using Eparafia.Application.ValueObjects;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Eparafia.Application.Actions.Parish;

public static class CreateParish
{
public sealed record Command(string Name, SimpleInfo SimpleInfo) : IRequest<Unit>;

public class Handler : IRequestHandler<Command, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public Handler(IUnitOfWork unitOfWork, IConfiguration configuration)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
    {
        return Unit.Value;
    }

    public sealed class Validator : AbstractValidator<Command>
    {
        public Validator()
        {

        }
    }
}
}