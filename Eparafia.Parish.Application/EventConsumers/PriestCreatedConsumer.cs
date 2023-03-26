using Eparafia.Application.EventConsumerActions.Priest.Command;
using MassTransit;
using MediatR;
using Shared.Messages;

namespace Eparafia.Application.EventConsumers;

public class PriestCreatedConsumer : IConsumer<PriestCreatedBusEvent>
{
    private readonly IMediator _mediator;

    public PriestCreatedConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public Task Consume(ConsumeContext<PriestCreatedBusEvent> context)
    {
        return _mediator.Send(new CreatePriest.Command(context.Message.PriestId, context.Message.Name,
            context.Message.PhotoPath, context.Message.PhotoPathMin));
    }
}