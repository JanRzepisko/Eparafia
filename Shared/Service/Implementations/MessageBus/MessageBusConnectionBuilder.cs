using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit;
using RawRabbit.Configuration;
using RawRabbit.Instantiation;
using Shared.Definitions;
using Shared.Service.Interfaces.MessageBus;

namespace Shared.Service.Implementations.MessageBus;

public sealed class MessageBusConnectionBuilder : IMessageBusConnectionBuilder<IBusClient>, IMessageBusConnectionBuilderConsumersAssemblyStage<IBusClient>, IMessageBusConnectionBuilderSubscribeStage<IBusClient>
{
    private IBusClient _BusClient;
    private IServiceCollection _Services;
    private Assembly _ConsumersAssembly;

    public MessageBusConnectionBuilder(IServiceCollection services)
    {
        _Services = services;
    }

    public IBusClient Build() => _BusClient;

    public IMessageBusConnectionBuilderConsumersAssemblyStage<IBusClient> ApplyConfiguration(
        IConfigurationSection configurationSection)
    {
        var brokerConfiguration = new RawRabbitConfiguration();
        configurationSection.Bind(brokerConfiguration);
        _BusClient =
            RawRabbitFactory.CreateSingleton(new RawRabbitOptions { ClientConfiguration = brokerConfiguration });
        return this;
    }

    public IMessageBusConnectionBuilderSubscribeStage<IBusClient> RegisterConsumersFromAssembly(
        Assembly consumersAssembly)
    {
        _ConsumersAssembly = consumersAssembly;
        return this;
    }

    public IMessageBusConnectionBuilderSubscribeStage<IBusClient> SubscribeToEvent<TEvent>()
        where TEvent : MessageBusEvent
    {
        var consumerType = _ConsumersAssembly
            .GetTypes()
            .FirstOrDefault(c => c.IsAssignableTo(typeof(IEventConsumer<TEvent>)));
        if (consumerType == null) return this;
        _Services.AddTransient(typeof(IEventConsumer<TEvent>), consumerType);
        _BusClient.SubscribeAsync<TEvent>(
            async @event => await _Services.BuildServiceProvider().GetRequiredService<IEventConsumerWrapper>()
                .ConsumeAsync(@event),
            c => c.UseSubscribeConfiguration(b =>
            {
                b.FromDeclaredQueue(a => a.WithName($"{_ConsumersAssembly.GetName().Name}/{typeof(TEvent).Name}"));
            })
        );
        return this;
    }
}