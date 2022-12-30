using Eparafia.Application.DataAccess;
using Eparafia.Infrastructure.Exceptions;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Eparafia.Application.Actions.PriestAuth.Command;

public static class RemovePriest
{
    public sealed record Command(Guid PriestId) : IRequest<Unit>;

    public class Handler : IRequestHandler<Command, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            bool exist = await _unitOfWork.Priests.ExistsAsync(request.PriestId, cancellationToken);
            if (!exist)
            {
                throw new EntityNotFoundException("Priest not found");
            }

            _unitOfWork.Priests.RemoveById(request.PriestId);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }

        public sealed class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(c => c.PriestId).NotEqual(Guid.Empty);
            }
        }
    }
}