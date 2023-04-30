using Eparafia.Administration.Domain.Enums;
using Shared.BaseModels.BaseEntities;

namespace Eparafia.Administration.Domain.Entities.BaptismParents;

public class BaptismParentsRelation : Entity
{
    public Guid BaptismRegisterId { get; set; }
    public BaptismRegister BaptismRegister { get; set; }
    public ParentRelation ParentRelation { get; set; }
    public Guid FatherId { get; set; }
    public BaptismFather Father { get; set; }
    public Guid MotherId { get; set; }
    public BaptismMother Mother { get; set; }
}