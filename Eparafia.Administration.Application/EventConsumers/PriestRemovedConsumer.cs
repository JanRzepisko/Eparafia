using Eparafia.Administration.Application.EventConsumerActions.Priest;
using MassTransit;
using MediatR;
using Shared.Messages;

namespace Eparafia.Administration.Application.EventConsumers;

public class PriestRemovedConsumer : IConsumer<PriestRemovedBusEvent>
{
    private readonly IMediator _mediator;

    public PriestRemovedConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public Task Consume(ConsumeContext<PriestRemovedBusEvent> context)
    {
        return _mediator.Send(new RemovePriest.Command(context.Message.PriestId));
    }
}