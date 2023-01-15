using Eparafia.Application.DataAccess.Abstract;
using Eparafia.Application.ValueObjects;

namespace Eparafia.Application.Entities;

public class SpecialEvent : Entity
{
    public Guid ParishId { get; set; }
    public Parish Parish { get; set; }
    public Event Event { get; set; }
    public DateTime Date { get; set; }
}