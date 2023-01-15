using Eparafia.Application.Entities;
using Eparafia.Application.ValueObjects;

namespace Eparafia.Application.DTOs;

public class EventDTO
{
    public DateTime Date { get; set; }
    public Event Event { get; set; }
    public Guid Id { get; set; }
    
    
    public static EventDTO FromEntity(SpecialEvent @event)
    {
        return new EventDTO
        {
            Id = @event.Id,
            Date = @event.Date,
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