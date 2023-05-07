namespace Shared.Messages;

public class ChangedParishPriestBusEvent
{
    public Guid? ParishId { get; set; }
    public Guid PriestId { get; set; }
}