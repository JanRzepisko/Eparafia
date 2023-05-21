using System.Reflection;
using Microsoft.Extensions.Configuration;
using Shared.Definitions;

namespace Shared.Service.Interfaces.MessageBus;


public interface IMessageBusConnectionBuilder<TBusClient>
{
    IMessageBusConnectionBuilderConsumersAssemblyStage<TBusClient> ApplyConfiguration(
        IConfigurationSection configurationSection);

    TBusClient Build();
}

public interface IMessageBusConnectionBuilderConsumersAssemblyStage<TBusClient>
{
    IMessageBusConnectionBuilderSubscribeStage<TBusClient> RegisterConsumersFromAssembly(Assembly consumersAssembly);
}

public interface IMessageBusConnectionBuilderSubscribeStage<TBusClient>
{
    IMessageBusConnectionBuilderSubscribeStage<TBusClient> SubscribeToEvent<TEvent>() where TEvent : MessageBusEvent;
    TBusClient Build();
}
