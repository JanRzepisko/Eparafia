using Shared.Definitions;

namespace Shared.Service.Interfaces.MessageBus;

internal interface IEventConsumerWrapper
{
    Task ConsumeAsync<TEvent>(TEvent @event) where TEvent : MessageBusEvent;
}