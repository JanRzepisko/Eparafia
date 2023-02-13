namespace Shared.Messages;

public class UserCreatedBusEvent
{
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public string PhotoPath { get; set; }
    public string PhotoPathMin { get; set; }
}