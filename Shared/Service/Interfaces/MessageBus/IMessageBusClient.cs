using Shared.Definitions;

namespace Shared.Service.Interfaces.MessageBus;

public interface IMessageBusClient
{
    Task SendAsync<TEvent>(TEvent @event,CancellationToken cancellationToken = default) where TEvent : MessageBusEvent;
}