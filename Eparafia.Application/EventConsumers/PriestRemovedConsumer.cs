using Eparafia.Application.Actions.Priest.Command;
using Eparafia.Application.EventConsumerActions.Priest.Command;
using MassTransit;
using MediatR;
using Shared.Messages;

namespace Eparafia.Application.EventConsumers;

public class PriestRemovedConsumer : IConsumer<PriestRemovedBusEvent>
{
    private readonly MediatR.IMediator _mediator;
    public PriestRemovedConsumer(IMediator mediator) => _mediator = mediator;
    
    public Task Consume(ConsumeContext<PriestRemovedBusEvent> context) => 
        _mediator.Send(new RemovePriest.Command(context.Message.PriestId));
}

