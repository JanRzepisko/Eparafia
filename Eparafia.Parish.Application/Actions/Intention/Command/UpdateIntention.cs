using Eparafia.Application.DataAccess;
using Eparafia.Domain.Enums;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Shared.BaseModels.Exceptions;

namespace Eparafia.Application.Actions.Intention.Command;

public static class UpdateIntention
{
    public sealed record Command
        (Guid IntentionId, string? Content, IntentionType? Type, DateTime? Date) : IRequest<Unit>;

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

            if (intention is null) throw new EntityNotFoundException(nameof(Intention), request.IntentionId);

            intention.Content = request.Content ?? intention.Content;
            intention.Type = request.Type ?? intention.Type;
            intention.Date = request.Date ?? intention.Date;
            if (request.Date != null || intention.Date != request.Date) intention.AutomaticAllocation = false;

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }

        public sealed class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(c => c.Date > DateTime.Now);
            }
        }
    }
}