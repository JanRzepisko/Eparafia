namespace Shared.Messages;

public class ChangeParishPriest
{
    public Guid? ParishId { get; set; }
    public Guid PriestId { get; set; }
}