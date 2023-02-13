namespace Shared.Messages;

public class PriestUpdatedBusEvent
{
    public Guid PriestId { get; set; }
    public string Name { get; set; }
    public string PhotoPath { get; set; }
    public string PhotoPathMin { get; set; }
}