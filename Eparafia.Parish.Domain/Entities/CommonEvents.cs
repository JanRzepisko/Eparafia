using Eparafia.Domain.ValueObjects;
using Shared.BaseModels.BaseEntities;

namespace Eparafia.Domain.Entities;

public class CommonEvent : Entity
{
    public Guid ParishId { get; set; }
    public Parish Parish { get; set; }
    public int EventInWeekId { get; set; }
    public Event Event { get; set; }
    public TimeSpan Time { get; set; }
    public DayOfWeek DayOfWeek { get; set; }
    public Guid? IntentionId { get; set; }
    public Intention Intention { get; set; }
}