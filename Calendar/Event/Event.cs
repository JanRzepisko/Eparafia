using eparafia.Calendar.EventEnums;

namespace eparafia.Calendar.Event;

public class Event
{
    public EventType Type { get; }
    public int Duration { get; }//min
    public string? Description { get; }
    
    public string Intention { get; }
    
    public Event(EventType type, int duration, string? description, string intention)
    {
        Type = type;
        Duration = duration;
        Description = description;
        Intention = intention;
    }
}