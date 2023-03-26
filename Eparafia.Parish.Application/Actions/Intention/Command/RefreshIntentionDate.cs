using Eparafia.Application.DataAccess;
using Eparafia.Application.Services;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Shared.BaseModels.Exceptions;

namespace Eparafia.Application.Actions.Intention.Command;

public static class RefreshIntentionDate
{
    public sealed record Command(Guid IntentionId) : IRequest<Unit>;

    public class Handler : IRequestHandler<Command, Unit>
    {
        private readonly IIntentionService _intentionService;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork, IConfiguration configuration, IIntentionService intentionService)
        {
            _unitOfWork = unitOfWork;
            _intentionService = intentionService;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var intention = await _unitOfWork.Intentions.GetByIdAsync(request.IntentionId, cancellationToken);

            if (intention is null) throw new EntityNotFoundException(nameof(Intention), request.IntentionId);

            intention.Date = await _intentionService.CalculateNextIntentionDateAsync(intention.ParishId,
                intention.Content, _unitOfWork, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }

        public sealed class Validator : AbstractValidator<Command>
        {
        }
    }
}