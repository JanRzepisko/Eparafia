using Shared.Definitions;

namespace Shared.Messages;

public class PriestRemovedBusEvent : MessageBusEvent
{
    public Guid PriestId { get; set; }
}