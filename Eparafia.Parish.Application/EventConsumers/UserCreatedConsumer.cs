using Eparafia.Application.EventConsumerActions.User.Command;
using MassTransit;
using MediatR;
using Shared.Messages;

namespace Eparafia.Application.EventConsumers;

public class UserCreatedConsumer : IConsumer<UserCreatedBusEvent>
{
    private readonly IMediator _mediator;

    public UserCreatedConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public Task Consume(ConsumeContext<UserCreatedBusEvent> context)
    {
        return _mediator.Send(new CreateUser.Command(context.Message.UserId, context.Message.Name,
            context.Message.PhotoPath, context.Message.PhotoPathMin));
    }
}