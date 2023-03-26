using Eparafia.Application.Actions.User.Command;
using MassTransit;
using MediatR;
using Shared.Messages;

namespace Eparafia.Application.EventConsumers;

public class UserUpdatedConsumer : IConsumer<UserUpdatedBusEvent>
{
    private readonly IMediator _mediator;

    public UserUpdatedConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public Task Consume(ConsumeContext<UserUpdatedBusEvent> context)
    {
        return _mediator.Send(new UpdateUser.Command(context.Message.UserId, context.Message.Name,
            context.Message.PhotoPath, context.Message.PhotoPathMin));
    }
}