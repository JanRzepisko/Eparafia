using Eparafia.Application.EventConsumerActions.Priest.Command;
using MediatR;
using Shared.Messages;
using Shared.Service.Interfaces.MessageBus;

namespace Eparafia.Application.EventConsumers;

public class PriestCreatedConsumer : IEventConsumer<PriestCreatedBusEvent>
{
    private readonly IMediator _mediator;

    public PriestCreatedConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public Task ConsumeAsync(PriestCreatedBusEvent @event, CancellationToken cancellationToken = default)
    {
        return _mediator.Send(new CreatePriest.Command(@event.PriestId, @event.Name, @event.PhotoPath, @event.PhotoPathMin), cancellationToken);
    }
}