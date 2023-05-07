using MassTransit;
using Shared.Definitions;

namespace Shared.EventBus;

public class EventBus : IEventBus
{
    private readonly IBus _bus;

    public EventBus(IBus bus)
    {
        _bus = bus;
    }

    public Task PublishAsync<T>(T message, CancellationToken cancellationToken) where T : MessageBusEvent
    {
        return _bus.Publish(message, cancellationToken);
    }
}