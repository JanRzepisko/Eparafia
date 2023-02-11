using MassTransit;

namespace Shared.EventBus;

public class EventBus : IEventBus
{
    private readonly IPublishEndpoint _publishEndpoint;

    public EventBus(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public Task PublishAsync<T>(T message, CancellationToken cancellationToken) where T : class => _publishEndpoint.Publish<T>(message, cancellationToken);
}