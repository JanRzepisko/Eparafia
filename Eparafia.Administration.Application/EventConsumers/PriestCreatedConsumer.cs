using Eparafia.Administration.Application.EventConsumerActions.Priest;
using MediatR;
using Shared.Messages;
using Shared.Service.Interfaces.MessageBus;

namespace Eparafia.Administration.Application.EventConsumers;

public class PriestCreatedConsumer : IEventConsumer<PriestCreatedBusEvent>
{
    private readonly IMediator _mediator;

    public PriestCreatedConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public Task ConsumeAsync(PriestCreatedBusEvent @event, CancellationToken cancellationToken = default)
    {
        return _mediator.Send(new CreatePriest.Command(@event.PriestId, @event.Name), cancellationToken);
    }
}