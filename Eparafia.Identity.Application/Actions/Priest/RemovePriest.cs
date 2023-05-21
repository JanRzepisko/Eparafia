using Eparafia.Identity.Application.DataAccess;
using FluentValidation;
using MediatR;
using Shared.BaseModels.Exceptions;
using Shared.Messages;
using Shared.Service.Interfaces.MessageBus;

namespace Eparafia.Identity.Application.Actions.Priest;

public static class RemovePriest
{
    public sealed record Command(Guid PriestId) : IRequest<Unit>;

    public class Handler : IRequestHandler<Command, Unit>
    {
        private readonly IMessageBusClient _messageBusClient;            
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork, IMessageBusClient messageBusClient)
        {
            _unitOfWork = unitOfWork;
            _messageBusClient = messageBusClient;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var exist = await _unitOfWork.Priests.ExistsAsync(request.PriestId, cancellationToken);
            if (!exist) throw new EntityNotFoundException("Priest not found");

            _unitOfWork.Priests.RemoveById(request.PriestId);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            await _messageBusClient.SendAsync(new PriestRemovedBusEvent
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