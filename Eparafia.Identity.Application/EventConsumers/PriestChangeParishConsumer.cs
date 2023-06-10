using Eparafia.Identity.Application.EventConsumerActions.Priest;
using MediatR;
using Shared.Messages;
using Shared.Service.Interfaces.MessageBus;

namespace Eparafia.Identity.Application.EventConsumers;

public class PriestChangeParishConsumer : IEventConsumer<ChangedParishPriestBusEvent>
{
    private readonly IMediator _mediator;

    public PriestChangeParishConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }
    public Task ConsumeAsync(ChangedParishPriestBusEvent @event, CancellationToken cancellationToken = default) => _mediator.Send(new ChangePriestParish.Command(@event.PriestId, @event.ParishId), cancellationToken);
}