using Eparafia.Administration.Application.EventConsumerActions.Priest;
using MassTransit;
using MediatR;
using Shared.Messages;

namespace Eparafia.Administration.Application.EventConsumers;

public class PriestChangeParishConsumer : IConsumer<ChangedParishPriestBusEvent>
{
    private readonly IMediator _mediator;

    public PriestChangeParishConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public Task Consume(ConsumeContext<ChangedParishPriestBusEvent> context)
    {
        return _mediator.Send(new ChangePriestParish.Command(context.Message.PriestId, context.Message.ParishId));
    }
}