using eparafia.Calendar.EventEnums;

namespace eparafia.Calendar.Event;

public class SpecialEvent : Event
{
    public DateTime Date { get; }

    public SpecialEvent(EventType type, int duration, string? description, DateTime date) : base(type, duration, description)
    {
        Date = date;
    }
}