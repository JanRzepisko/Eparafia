using Eparafia.Application.Actions.Priest.Command;
using MediatR;
using Shared.Messages;
using Shared.Service.Interfaces.MessageBus;

namespace Eparafia.Application.EventConsumers;

public class PriestRemovedConsumer : IEventConsumer<PriestRemovedBusEvent>
{
    private readonly IMediator _mediator;

    public PriestRemovedConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    public Task ConsumeAsync(PriestRemovedBusEvent @event, CancellationToken cancellationToken = default)
    {
        return _mediator.Send(new RemovePriest.Command(@event.PriestId), cancellationToken);
    }
}