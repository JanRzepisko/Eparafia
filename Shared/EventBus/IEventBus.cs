using Shared.Definitions;

namespace Shared.EventBus;

public interface IEventBus
{
    Task PublishAsync<T>(T message, CancellationToken cancellationToken) where T : MessageBusEvent;
}