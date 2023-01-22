using Eparafia.Application.DataAccess;
using Eparafia.Application.Entities;
using Eparafia.Application.Enums;
using Eparafia.Application.Services;
using Eparafia.Infrastructure.Exceptions;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Eparafia.Application.Actions.Parish;

public static class RefreshIntentionDate
{
    public sealed record Command(Guid IntentionId) : IRequest<Unit>;

    public class Handler : IRequestHandler<Command, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IIntentionService _intentionService;

        public Handler(IUnitOfWork unitOfWork, IConfiguration configuration, IIntentionService intentionService)
        {
            _unitOfWork = unitOfWork;
            _intentionService = intentionService;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var intention = await _unitOfWork.Intentions.GetByIdAsync(request.IntentionId, cancellationToken);
            
            if (intention is null)
            {
                throw new EntityNotFoundException(nameof(Intention), request.IntentionId);
            }
            
            intention.Date = await _intentionService.CalculateNextIntentionDateAsync(intention.ParishId, intention.Content, _unitOfWork, cancellationToken);
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