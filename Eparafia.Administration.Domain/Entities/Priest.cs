using Shared.BaseModels.BaseEntities;

namespace Eparafia.Administration.Domain.Entities;

public class Priest : Entity
{
    public string Name { get; set; }
    public Guid ParishId { get; set; }
    public Parish Parish { get; set; }
}