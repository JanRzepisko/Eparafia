using Eparafia.Domain.ValueObjects;
using Shared.BaseModels.BaseEntities;

namespace Eparafia.Domain.Entities;

public class SpecialEvent : Entity
{
    public Guid ParishId { get; set; }
    public Parish Parish { get; set; }
    public Event Event { get; set; }
    public DateTime Date { get; set; }
    public Intention? Intention { get; set; }
}