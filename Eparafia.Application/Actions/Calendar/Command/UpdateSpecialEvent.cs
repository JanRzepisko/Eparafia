using Eparafia.Application.DataAccess;
using Eparafia.Application.Enums;
using Eparafia.Domain.Enums;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Eparafia.Application.Actions.Parish;

public static class UpdateSpecialEvent
{
    public sealed record Command(Guid EventId, EventType? Type, string? Description, string? Name, DateTime? Date, int? Duration) : IRequest<Unit>;

    public class Handler : IRequestHandler<Command, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var specialEvent = await _unitOfWork.SpecialEvents.GetByIdAsync(request.EventId, cancellationToken);
            if (specialEvent is null)
            {
                throw new Exception("Special event not found");
            }

            specialEvent.Event.Name = request.Name ?? specialEvent.Event.Name;
            specialEvent.Event.Description = request.Description ?? specialEvent.Event.Description;
            specialEvent.Event.Type = request.Type ?? specialEvent.Event.Type;
            specialEvent.Event.Duration = request.Duration ?? specialEvent.Event.Duration;
            specialEvent.Date = request.Date ?? specialEvent.Date;
            
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