using Eparafia.Application.EventConsumerActions.Priest.Command;
using MediatR;
using Shared.Messages;
using Shared.Service.Interfaces.MessageBus;

namespace Eparafia.Application.EventConsumers;

public class PriestUpdatedConsumer : IEventConsumer<PriestUpdatedBusEvent>
{
    private readonly IMediator _mediator;

    public PriestUpdatedConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }
    public Task ConsumeAsync(PriestUpdatedBusEvent @event, CancellationToken cancellationToken = default)
    {
        return _mediator.Send(new UpdatePriest.Command(@event.PriestId, @event.Name, @event.PhotoPath, @event.PhotoPathMin), cancellationToken);     
    }
}