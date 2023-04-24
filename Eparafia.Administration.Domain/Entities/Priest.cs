using Shared.BaseModels.BaseEntities;

namespace Eparafia.Administration.Domain.Entities;

public class Priest : Entity
{
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public Guid ParishId { get; set; }
    public Parish Parish { get; set; }
}