using Eparafia.Application.DataAccess;
using Eparafia.Infrastructure.Exceptions;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Eparafia.Application.Actions.Parish;

public static class RemoveSpecialEvent
{
    public sealed record Command(Guid SpecialEventId) : IRequest<Unit>;

    public class Handler : IRequestHandler<Command, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var specialEvent = await _unitOfWork.SpecialEvents.GetByIdAsync(request.SpecialEventId, cancellationToken);
            if (specialEvent is null)
            {
                throw new EntityNotFoundException("Special event not found");
            }
            
            _unitOfWork.SpecialEvents.Remove(specialEvent);
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