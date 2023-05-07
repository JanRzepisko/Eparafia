using Shared.Definitions;

namespace Shared.Messages;

public class PriestUpdatedBusEvent : MessageBusEvent
{
    public Guid PriestId { get; set; }
    public string Name { get; set; }
    public string PhotoPath { get; set; }
    public string PhotoPathMin { get; set; }
}