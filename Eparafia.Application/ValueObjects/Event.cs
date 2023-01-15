using Eparafia.Application.Enums;

namespace Eparafia.Application.ValueObjects;

public class Event : ValueObject
{
    public EventType Type { get; set; }
    public string EventDescription { get; set; }
    public string Name { get; set; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Type;
        yield return EventDescription;
        yield return Name;
        
    }
}