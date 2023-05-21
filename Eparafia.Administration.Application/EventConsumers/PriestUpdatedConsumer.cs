using Eparafia.Administration.Application.EventConsumerActions.Priest;
using MediatR;
using Shared.Messages;
using Shared.Service.Interfaces.MessageBus;

namespace Eparafia.Administration.Application.EventConsumers;

public class PriestUpdatedConsumer : IEventConsumer<PriestUpdatedBusEvent>
{
    private readonly IMediator _mediator;

    public PriestUpdatedConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }
    public Task ConsumeAsync(PriestUpdatedBusEvent @event, CancellationToken cancellationToken = default)
    {
        return _mediator.Send(new UpdatePriest.Command(@event.PriestId, @event.Name), cancellationToken);
    }
}