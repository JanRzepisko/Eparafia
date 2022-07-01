using eparafia.Calendar.EventEnums;

namespace eparafia.Calendar.Event;

public class SpecialEvent : Event
{
    public DateTime Date { get; }

    public SpecialEvent(EventType type, int duration, string? description, DateTime date, Intention.Intention intention) : base(type, duration, description, intention)
    {
        Date = date;
    }
}