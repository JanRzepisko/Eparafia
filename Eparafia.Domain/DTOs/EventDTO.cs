using Eparafia.Domain.Entities;
using Eparafia.Domain.ValueObjects;

namespace Eparafia.Domain.DTOs;

public class EventDTO
{
    public DateTime Date { get; set; }
    public Event Event { get; set; }
    public bool IsSpecial { get; set; }
    public IntentionDTO? Intention { get; set; }
    
    
    public static EventDTO FromEntity(SpecialEvent @event)
    {
        return new EventDTO
        {
            Date = @event.Date,
            IsSpecial = @event.Id != Guid.Empty,
            Intention = IntentionDTO.FromEntity(@event.Intention),
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