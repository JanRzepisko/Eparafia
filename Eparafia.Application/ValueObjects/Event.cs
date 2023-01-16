using Eparafia.Application.Enums;

namespace Eparafia.Application.ValueObjects;

public class Event : ValueObject
{
    public EventType Type { get; set; }
    public string Description { get; set; }
    public string Name { get; set; }
    public int Duration { get; set; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Type;
        yield return Description;
        yield return Name;
        
    }
}