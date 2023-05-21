using Shared.Definitions;

namespace Shared.Messages;

public class UserRemovedBusEvent : MessageBusEvent
{
    public Guid Id { get; set; }
}