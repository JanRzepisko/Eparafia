using Shared.Definitions;

namespace Shared.Messages;

public class UserUpdatedBusEvent : MessageBusEvent
{
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public string PhotoPath { get; set; }
    public string PhotoPathMin { get; set; }
}