using Microsoft.Extensions.DependencyInjection;
using Shared.Definitions;
using Shared.Service.Interfaces.MessageBus;

namespace Shared.Service.Implementations.MessageBus;

internal sealed class EventConsumerWrapper : IEventConsumerWrapper
{
    private readonly IServiceProvider _ServiceProvider;
    public EventConsumerWrapper(IServiceProvider serviceProvider)
    {
        _ServiceProvider = serviceProvider;
    }
    public async Task ConsumeAsync<TEvent>(TEvent @event) where TEvent : MessageBusEvent
    {
        using var servicesScope = _ServiceProvider.CreateScope();
        var consumerInstance = servicesScope.ServiceProvider.GetRequiredService<IEventConsumer<TEvent>>();
        if (consumerInstance is not null)
        {
            Console.WriteLine($"EventConsumer | Consumer : {consumerInstance.GetType().FullName} | Event : {typeof(TEvent).Name}");
            try
            {
                await consumerInstance.ConsumeAsync(@event);
            }
            catch (Exception e)
            {
                Console.WriteLine($"EventConsumer | Consumer : {consumerInstance.GetType().FullName} | Event : {typeof(TEvent).Name} | \nException : {e.Message}");
            }
        }
    }
}