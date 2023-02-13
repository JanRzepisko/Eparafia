using Eparafia.Application.Actions.User.Command;
using MassTransit;
using MediatR;
using Shared.Messages;

namespace Eparafia.Application.EventConsumers;

public class UserRemovedConsumer : IConsumer<UserUpdatedBusEvent>
{
    private readonly MediatR.IMediator _mediator;
    public UserRemovedConsumer(IMediator mediator) => _mediator = mediator;
    
    public Task Consume(ConsumeContext<UserUpdatedBusEvent> context) => 
        _mediator.Send(new RemoveUser.Command(context.Message.UserId));
}

