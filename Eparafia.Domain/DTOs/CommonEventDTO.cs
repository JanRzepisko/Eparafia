using Eparafia.Domain.Entities;
using Eparafia.Domain.ValueObjects;

namespace Eparafia.Domain.DTOs;

public class CommonEventDTO
{
    public DayOfWeek DayOfWeek { get; set; }
    public int Hour { get; set; }
    public int Minute { get; set; }
    public Event Event { get; set; }
    public Guid Id { get; set; }
    
    
    public static CommonEventDTO FromEntity(CommonEvent @event)
    {
        return new CommonEventDTO
        {
            Id = @event.Id,
            DayOfWeek = @event.DayOfWeek,
            Hour = @event.Time.Hours,
            Minute = @event.Time.Minutes,
            Event = new Event
            {
                Name = @event.Event.Name,
                Description = @event.Event.Description,
                Duration = @event.Event.Duration,
                Type = @event.Event.Type
            }
        };
    }
}