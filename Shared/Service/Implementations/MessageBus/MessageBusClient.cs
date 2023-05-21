using Shared.Definitions;
using Shared.Service.Interfaces.MessageBus;
using RawRabbit;

namespace Shared.Service.Implementations.MessageBus;

internal sealed class MessageBusClient : IMessageBusClient
{        
    private readonly IBusClient _BusClient;
    public MessageBusClient(IBusClient busClient)
    {
        _BusClient = busClient;
    }

    public Task SendAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default) where TEvent : MessageBusEvent
    {
        return _BusClient.PublishAsync(@event, token: cancellationToken);
    }
}