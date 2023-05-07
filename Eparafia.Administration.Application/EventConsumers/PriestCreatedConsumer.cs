using Eparafia.Administration.Application.EventConsumerActions.Priest;
using MassTransit;
using MediatR;

namespace Eparafia.Administration.Application.EventConsumers;

public class PriestCreatedConsumer : IConsumer<Shared.Messages.PriestCreatedBusEvent>
{
    private readonly IMediator _mediator;

    public PriestCreatedConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }
    public Task Consume(ConsumeContext<Shared.Messages.PriestCreatedBusEvent> context)
    {
        return _mediator.Send(new CreatePriest.Command(context.Message.PriestId, context.Message.Name));
    }
}