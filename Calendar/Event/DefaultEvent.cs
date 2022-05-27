using eparafia.Calendar.EventEnums;

namespace eparafia.Calendar.Event;

public class DefaultEvent : Event
{
    public DayOfWeek Day { get; }
    public string? Time{ get; }


    public DefaultEvent(EventType type, string? time, int duration, string? description, DayOfWeek day, string intention) : base(type, duration, description, intention)
    {
        Day = day;
        Time = time;
    }
}