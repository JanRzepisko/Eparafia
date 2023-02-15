using Eparafia.Identity.Application.DataAccess;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Shared.BaseModels.Exceptions;
using Shared.EventBus;
using Shared.Messages;

namespace Eparafia.Identity.Application.Actions.Priest;

public static class RemovePriest
{
    public sealed record Command(Guid PriestId) : IRequest<Unit>;

    public class Handler : IRequestHandler<Command, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventBus _eventBus;

        public Handler(IUnitOfWork unitOfWork, IConfiguration configuration, IEventBus eventBus)
        {
            _unitOfWork = unitOfWork;
            _eventBus = eventBus;
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
            
            await _eventBus.PublishAsync(new PriestRemovedBusEvent()
            {
                PriestId = request.PriestId
            }, cancellationToken);
            
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