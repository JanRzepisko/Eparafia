using Shared.BaseModels.BaseEntities;

namespace Eparafia.Domain.Entities;

public class Payment : Entity
{
    public Guid ParishId { get; set; }
    public Parish Parish { get; set; }
    public decimal Amount { get; set; }
}