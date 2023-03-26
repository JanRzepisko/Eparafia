using Eparafia.Application.EventConsumerActions.Priest.Command;
using MassTransit;
using MediatR;
using Shared.Messages;

namespace Eparafia.Application.EventConsumers;

public class PriestUpdatedConsumer : IConsumer<PriestUpdatedBusEvent>
{
    private readonly IMediator _mediator;

    public PriestUpdatedConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public Task Consume(ConsumeContext<PriestUpdatedBusEvent> context)
    {
        return _mediator.Send(new UpdatePriest.Command(context.Message.PriestId, context.Message.Name,
            context.Message.PhotoPath, context.Message.PhotoPathMin));
    }
}