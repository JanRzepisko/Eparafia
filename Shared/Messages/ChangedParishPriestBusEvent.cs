using Shared.Definitions;

namespace Shared.Messages;

public class ChangedParishPriestBusEvent : MessageBusEvent
{
    public Guid? ParishId { get; set; }
    public Guid PriestId { get; set; }
}