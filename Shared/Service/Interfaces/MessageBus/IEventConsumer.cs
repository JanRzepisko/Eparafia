using Shared.Definitions;

namespace Shared.Service.Interfaces.MessageBus;

public interface IEventConsumer<TEvent> where TEvent : MessageBusEvent
{
    Task ConsumeAsync(TEvent @event, CancellationToken cancellationToken = default);
}
