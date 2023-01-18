using Eparafia.Application.DataAccess;
using Eparafia.Application.Entities;
using Eparafia.Infrastructure.Exceptions;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Eparafia.Application.Actions.Parish;

public static class RemoveIntention
{
    public sealed record Command(Guid IntentionId) : IRequest<Unit>;

    public class Handler : IRequestHandler<Command, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var intention = await _unitOfWork.Intentions.GetByIdAsync(request.IntentionId, cancellationToken);
            
            if (intention is null)
            {
                throw new EntityNotFoundException(nameof(Intention), request.IntentionId);
            }
            
            _unitOfWork.Intentions.Remove(intention);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
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